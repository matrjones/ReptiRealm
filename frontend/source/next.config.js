const version = process.env.BUILD_VERSION;
/** @type {import('next').NextConfig} */

const environment = process.env.ENVIRONMENT_NAME ?? "dev";
const isDev = environment === "dev";
console.log(`BUILD VERSION: ${version}`);

// Log environment variables for debugging
console.log("STRIPE_SECRET_KEY available:", !!process.env.STRIPE_SECRET_KEY);
console.log("STRIPE_WEBHOOK_SECRET available:", !!process.env.STRIPE_WEBHOOK_SECRET);

const nextConfig = {
  reactStrictMode: false,
  output: "standalone",
  experimental: {
    reactCompiler: true,
  },
  compress: true,
  env: {
    STRIPE_SECRET_KEY: process.env.STRIPE_SECRET_KEY,
    STRIPE_WEBHOOK_SECRET: process.env.STRIPE_WEBHOOK_SECRET,
    STRIPE_MONTHLY_PRICE_ID: process.env.STRIPE_MONTHLY_PRICE_ID,
    STRIPE_YEARLY_PRICE_ID: process.env.STRIPE_YEARLY_PRICE_ID,
  },
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