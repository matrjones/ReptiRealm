#!/bin/bash -e

. ./scripts/web/set-env.sh $1

pushd "${PWD}/source"
ls -a
aws s3 cp ./.next/static s3://traveltrekker-images-${ENVIRONMENT_NAME}/${BUILD_VERSION}/_next/static --recursive
