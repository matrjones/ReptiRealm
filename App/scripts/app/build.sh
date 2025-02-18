#!/bin/bash
set -e  

BUILD_ENV=$1  

echo "Building Flutter project for environment: $BUILD_ENV"

cd App

flutter clean
flutter pub get

echo "Building Android APK..."
flutter build apk --release

echo "Building Android App Bundle..."
flutter build appbundle --release


echo "Build completed successfully. Artifacts moved to 'build_output/'"
