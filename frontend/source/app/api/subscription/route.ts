import { NextResponse } from "next/server";
import { NextRequest } from "next/server";

export async function GET(request: NextRequest) {
  try {
    const authHeader = request.headers.get("authorization");

    if (!authHeader) {
      return NextResponse.json({ error: "Unauthorized" }, { status: 401 });
    }

    const response = await fetch(
      `${process.env.NEXT_PUBLIC_API_URL}/Subscription/Get`,
      {
        headers: {
          Authorization: authHeader,
        },
      }
    );

    if (!response.ok) {
      throw new Error("Failed to fetch subscription");
    }

    const subscription = await response.json();

    return NextResponse.json({
      subscription: subscription
        ? {
            status: subscription.status,
            plan: {
              name: subscription.planName,
              interval: subscription.interval,
            },
          }
        : null,
    });
  } catch (error) {
    console.error("Error fetching subscription:", error);
    return NextResponse.json(
      { error: "Failed to fetch subscription" },
      { status: 500 }
    );
  }
}
