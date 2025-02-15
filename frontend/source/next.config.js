const version = process.env.BUILD_VERSION;
/** @type {import('next').NextConfig} */

const environment = process.env.ENVIRONMENT_NAME ?? "dev";
const isDev = environment === "dev";
console.log(`BUILD VERSION: ${version}`);

const nextConfig = {
  reactStrictMode: false,
  output: "standalone",
  experimental: {
    reactCompiler: true,
  },
  compress: true,
  assetPrefix: isDev
    ? undefined
    : process.env.ENVIRONMENT_NAME === "stage"
    ? `https://repti-realm-images-stage.s3.eu-west-1.amazonaws.com/${version}`
    : `https://repti-realm-images-stage.s3.eu-west-1.amazonaws.com/${version}`,
  images: {
    unoptimized: false,
    remotePatterns: [
    ],
  },
  pageExtensions: ["js", "jsx", "ts", "tsx", "graphql"],
};
module.exports = nextConfig;