import type { Config } from "tailwindcss";

const config: Config = {
  content: [
    "./components/**/*.{js,ts,jsx,tsx,mdx}",
    "./app/**/*.{js,ts,jsx,tsx,mdx}",
  ],
  theme: {
    extend: {
      fontFamily: {
        quicksand: ["Quicksand", "sans-serif"],
      },
      screens: {
        xs: "414px",
        sm: "768px",
        md: "962px",
        lg: "1280px",
        xl: "1440px",
        xxl: "1920px",
      },
      colors: {
        light: {
          background: "#F8F8F8",
          text: "#0B0B0B",
          link: "#FFA726",
        },
        dark: {
          background: "#121212",
          text: "#dfdfdf",
          link: "#9c5d00",
        },
        primary: "#F8F8F8",
        secondary: "#0B0B0B",
        accent: "#f5e1af",
        accenthover: "#28666e",
      },
      backgroundImage: {
        "gradient-radial": "radial-gradient(var(--tw-gradient-stops))",
        "gradient-conic":
          "conic-gradient(from 180deg at 50% 50%, var(--tw-gradient-stops))",
        "sandy-gradient":
          "linear-gradient(to right bottom, #fffdf6, #fffaec, #fff7e1, #fff4d7, #fff1cd)",
        "gradient-white":
          "linear-gradient(45deg, rgba(255,255,255,1) 15%, rgba(255,255,255,0.3) 75%);",
        "gradient-black":
          "linear-gradient(to bottom, rgba(0, 0, 0, 0.75) 0%, rgba(0, 0, 0, 0) 85%)",
        "gradient-orange":
          "linear-gradient(to bottom, rgba(222, 115, 22, 0.85) 35%, rgba(255, 140, 0, 0) 85%)",
        "gradient-orange-1":
          "linear-gradient(to bottom, rgba(0, 0, 0, 0.6) 20%, rgba(255, 140, 0, 0) 85%)",
        "gradient-orange-2":
          "linear-gradient(to bottom, rgba(0, 0, 0, 0.6) 20%, rgba(255, 140, 0, 0) 85%)",
        "gradient-orange-3":
          "linear-gradient(to bottom, rgba(0, 0, 0, 0.6) 20%, rgba(255, 140, 0, 0) 85%)",
        "gradient-orange-4":
          "linear-gradient(to bottom, rgba(0, 0, 0, 0.6) 20%, rgba(255, 140, 0, 0) 85%)",
      },
      underlineOffset: {
        4: "4px",
        8: "8px",
      },
    },
  },
  plugins: [],
  darkMode: "class",
};
export default config;
