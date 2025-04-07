import { cookies } from "next/headers";

export async function GET() {
    const cookieStorage = await cookies()
    const cookieValue = cookieStorage.get("isAuthenticated")?.value
    const isAuthenticated = cookieValue === "true";

    return Response.json(isAuthenticated)
}
