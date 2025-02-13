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
    ? `https://stage-images.repti-realm.co.uk/${version}`
    : `https://prod-images.repti-realm.co.uk/${version}`,
  images: {
    unoptimized: false,
    remotePatterns: [
    ],
  },
  pageExtensions: ["js", "jsx", "ts", "tsx", "graphql"],
};
module.exports = nextConfig;