The primary focus of unit testing is domain classes and also classes that may not get well
exercised in integration tests but could cause obscure failures. 

Integration testing is initially being performed at a subcutaneous level by dispatching commands
rather than the traditional full-stack method of instantiating an HTTP server and client to test
responses to requests. It is being done this way as the current UI during development is temporary
and will be replaced by either a significantly enhanced RazorPages site or converted to an
API/SPA type system. Razor Page models and command validation will rely on integration testing.

The level of unit testing versus integration testing for Command Handlers is not yet decided.