import type { CodegenConfig } from "@graphql-codegen/cli";

const config: CodegenConfig = {
  schema: "http://localhost:3005/graphql",
  overwrite: true,
  documents: "**/*.ts",
  generates: {
    "./graphql/__generated__/": {
      preset: "client",
      plugins: [],
      presetConfig: {
        fragmentMasking: false,
      },
    },
  },
};

export default config;
