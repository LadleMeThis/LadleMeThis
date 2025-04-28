/** @type {import('next').NextConfig} */
const nextConfig = {
  async rewrites() {
    return [
      {
        source: '/api/:path*',
        destination: 'http://localhost:8080/:path*', 
      },
    ];
  },
  images: {
    domains: ['images.pexels.com'],
  },
};

export default nextConfig;
