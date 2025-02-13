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
    ? `https://stage-images.traveltrekker.co.uk/${version}`
    : `https://prod-images.traveltrekker.co.uk/${version}`,
  images: {
    unoptimized: false,
    remotePatterns: [
      {
        protocol: "https",
        hostname: "stage-images.traveltrekker.co.uk",
        port: "",
        pathname: "/images/**",
      },
      {
        protocol: "https",
        hostname: "prod-images.traveltrekker.co.uk",
        port: "",
        pathname: "/images/**",
      },
      {
        protocol: "https",
        hostname: "traveltrekker.s3.eu-west-1.amazonaws.com",
        port: "",
        pathname: "/images/**",
      },
      {
        protocol: "https",
        hostname: "lh5.googleusercontent.com",
        port: "",
        pathname: "/**",
      },
    ],
  },
  pageExtensions: ["js", "jsx", "ts", "tsx", "graphql"],
};
module.exports = nextConfig;