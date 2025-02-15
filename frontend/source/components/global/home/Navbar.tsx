export default function Navbar() {
  return (
    <nav className="relative flex items-center justify-between md:justify-center py-6 px-4 mt-2">
      <div className="flex items-center flex-1 md:absolute md:inset-y-0 md:left-0">
        <div className="flex items-center ml-3 justify-between w-full md:w-auto">
          <a
            className="flex items-center justify-center"
            href=""
            aria-label="Home"
          >
            <img src="/logo.svg" height="60" width="60" />
            <h2 className="text-2xl ml-2 font-thin">REPTIREALM</h2>
          </a>
          <div className="-mr-2 flex items-center md:hidden">
            <button
              type="button"
              id="main-menu"
              aria-label="Main menu"
              aria-haspopup="true"
              className="inline-flex items-center justify-center p-2 rounded-md text-gray-400 hover:text-gray-500 hover:bg-gray-100 focus:outline-none focus:bg-gray-100 focus:text-gray-500 transition duration-150 ease-in-out"
            >
              <svg
                stroke="currentColor"
                fill="none"
                viewBox="0 0 24 24"
                className="h-6 w-6"
              >
                <path
                  stroke-linecap="round"
                  stroke-linejoin="round"
                  stroke-width="2"
                  d="M4 6h16M4 12h16M4 18h16"
                ></path>
              </svg>
            </button>
          </div>
        </div>
      </div>
      <div className="hidden md:flex md:space-x-10">
        <a
          href="#features"
          className="font-medium  hover:text-gray-900 transition duration-150 ease-in-out"
        >
          Features
        </a>
        <a
          href="#pricing"
          className="font-medium  hover:text-gray-900 transition duration-150 ease-in-out"
        >
          Pricing
        </a>
        <a
          href="/blog"
          className="font-medium  hover:text-gray-900 transition duration-150 ease-in-out"
        >
          Blog
        </a>
        <a
          href="https://docs.pingping.io"
          target="_blank"
          className="font-medium  hover:text-gray-900 transition duration-150 ease-in-out"
        >
          Docs
        </a>
      </div>
      <div className="mr-4 hidden md:absolute md:flex md:items-center md:justify-end md:inset-y-0 md:right-0">
        <span className="inline-flex">
          <a
            href="/login"
            className="inline-flex items-center px-4 py-2 border border-transparent text-base leading-6 font-medium text-orange-600 hover:text-orange-500 focus:outline-none transition duration-150 ease-in-out"
          >
            Login
          </a>
        </span>
        <span className="inline-flex rounded-md shadow ml-2">
          <a
            href="/signup"
            className="inline-flex items-center px-4 py-2 border border-transparent text-base leading-6 font-medium rounded-md text-white bg-orange-600 hover:bg-orange-500 focus:outline-none focus:border-blue-700 transition duration-150 ease-in-out"
          >
            Get started
          </a>
        </span>
      </div>
    </nav>
  );
}
