import { NextResponse } from "next/server";
import type { NextRequest } from "next/server";

// List of paths that don't require authentication
const publicPaths = [
  "/login",
  "/register",
  "/",
  "/guides",
  "/community",
  "/blog",
];

// List of API paths that don't require authentication
const publicApiPaths = ["/api/identity/login", "/api/identity/register"];

export function middleware(request: NextRequest) {
  const token = request.cookies.get("token")?.value;
  const { pathname } = request.nextUrl;

  // Log the request for debugging
  console.log("Middleware processing request:", {
    pathname,
    hasToken: !!token,
    isApiRequest: pathname.startsWith("/api"),
  });

  // Check if the path is public
  const isPublicPath = publicPaths.some((path) => pathname.startsWith(path));
  const isPublicApiPath = publicApiPaths.some((path) =>
    pathname.startsWith(path)
  );

  // If it's a public path or API endpoint, allow access
  if (isPublicPath || isPublicApiPath) {
    console.log("Allowing access to public path:", pathname);
    return NextResponse.next();
  }

  // If there's no token and it's not a public path, redirect to login
  if (!token) {
    console.log("No token found, redirecting to login");
    const loginUrl = new URL("/login", request.url);
    loginUrl.searchParams.set("redirect", pathname);
    return NextResponse.redirect(loginUrl);
  }

  // If there's a token, verify it's not expired
  try {
    const tokenData = JSON.parse(atob(token.split(".")[1]));
    const expirationTime = tokenData.exp * 1000; // Convert to milliseconds
    const currentTime = Date.now();

    console.log("Token expiration check:", {
      expirationTime,
      currentTime,
      isExpired: currentTime >= expirationTime,
    });

    if (currentTime >= expirationTime) {
      console.log("Token expired, redirecting to login");
      // Token is expired, redirect to login
      const loginUrl = new URL("/login", request.url);
      loginUrl.searchParams.set("redirect", pathname);
      loginUrl.searchParams.set("expired", "true");
      return NextResponse.redirect(loginUrl);
    }

    // For API requests, add the token to the request headers
    if (pathname.startsWith("/api")) {
      console.log("Adding token to API request headers");
      const requestHeaders = new Headers(request.headers);
      requestHeaders.set("Authorization", `Bearer ${token}`);

      return NextResponse.next({
        request: {
          headers: requestHeaders,
        },
      });
    }
  } catch (error) {
    console.error("Error parsing token:", error);
    // If token is invalid, redirect to login
    const loginUrl = new URL("/login", request.url);
    loginUrl.searchParams.set("redirect", pathname);
    return NextResponse.redirect(loginUrl);
  }

  return NextResponse.next();
}

// Configure which paths the middleware should run on
export const config = {
  matcher: [
    /*
     * Match all request paths except for the ones starting with:
     * - _next/static (static files)
     * - _next/image (image optimization files)
     * - favicon.ico (favicon file)
     */
    "/((?!_next/static|_next/image|favicon.ico).*)",
  ],
};
