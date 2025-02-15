#!/bin/bash -e

. ./frontend/scripts/web/set-env.sh $1

pushd "${PWD}/frontend/source"
ls -a
aws s3 cp ./.next/static s3://repti-realm-images-${ENVIRONMENT_NAME}/${BUILD_VERSION}/_next/static --recursive
