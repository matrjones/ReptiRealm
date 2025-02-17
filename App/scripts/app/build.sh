#!/bin/bash
set -e  

BUILD_ENV=$1  

echo "Building Flutter project for environment: $BUILD_ENV"

cd app

flutter clean
flutter pub get

echo "Building Android APK..."
flutter build apk --release

echo "Building Android App Bundle..."
flutter build appbundle --release

mkdir -p build_output
mv build/app/outputs/flutter-apk/app-release.apk build_output/
mv build/app/outputs/bundle/release/app-release.aab build_output/

echo "Build completed successfully. Artifacts moved to 'build_output/'"
