import { NextResponse } from "next/server";
import { NextRequest } from "next/server";
import axios from "axios";

export async function GET(req: NextRequest) {
  try {
    const authHeader = req.headers.get("Authorization");

    if (!authHeader) {
      return new NextResponse("Unauthorized", { status: 401 });
    }

    // Get customer email from your backend
    console.log(authHeader);
    console.log(process.env.NEXT_PUBLIC_API_URL);
    console.log(process.env.NEXT_PUBLIC_API_URL + "/Identity/Get");

    const userResponse = await axios.get(
      `${process.env.NEXT_PUBLIC_API_URL}/Identity/Get`,
      {
        headers: {
          authorization: authHeader,
        },
        httpsAgent: new (require("https").Agent)({
          rejectUnauthorized: false,
        }),
      }
    );

    console.log(userResponse);

    if (userResponse.status !== 200) {
      return new NextResponse("Failed to get user information", {
        status: 401,
      });
    }

    const user = userResponse.data;
    console.log(user.email);
    console.log("hit");

    return NextResponse.json({
      email: user.email,
    });
  } catch (error) {
    console.error("Error creating subscription:", error);
    return new NextResponse("Internal Server Error", { status: 500 });
  }
}
