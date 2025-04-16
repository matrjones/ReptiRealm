#!/bin/bash -e

. ./frontend/scripts/web/set-env.sh $1

pushd "${PWD}/frontend/source"

if [ "$ENVIRONMENT_NAME" == "stage" ]; then
    rm -f ./.env.production
    mv ./.env.staging ./.env.production
fi

pnpm install
pnpm build


# Copy artifacts for deployment
cp -r .next/standalone/. $ARTIFACTS_DIR
cp run.sh $ARTIFACTS_DIR

# Ensure /tmp/cache directory exists
[ ! -d '/tmp/cache' ] && mkdir -p /tmp/cache

# Remove existing symbolic link or directory to avoid conflicts
[ -L "$ARTIFACTS_DIR/.next/cache" ] && rm "$ARTIFACTS_DIR/.next/cache"
[ -d "$ARTIFACTS_DIR/.next/cache" ] && rm -rf "$ARTIFACTS_DIR/.next/cache"

# Create symbolic link to /tmp/cache
ln -s /tmp/cache "$ARTIFACTS_DIR/.next/cache"

cd $ARTIFACTS_DIR && zip -ry ../lambda.zip .

popd