import { NextResponse } from 'next/server';

export async function middleware(req) {
    const token = req.cookies.get('AuthToken');

    const response = NextResponse.next();

    if (token) {
        response.cookies.set('isAuthenticated', 'true', { path: '/' });
    } else {
        response.cookies.set('isAuthenticated', 'false', { path: '/' });
    }

    return response;
}

export const config = {
    matcher: ['/profile', '/create', '/modify'],
};
