Currently onlny works with .Net Core 1.1, as IdentityServer isn't yet working with .Net Core 2.0. Use [this](https://reynders.co/use-this-helper-cli-for-switching-net-core-sdk-versions/) to target the correct version.

The solution has three projects:

 - IdentityServer (port 5000): The main identity server used for auth.
 - APIApplication (port 5002): A Web API application that requires authorization.
 - WebApplication (port 5001): An MVC web application that has a page that requires auth, along with the ability to call the API.


## Useful urls

- [http://localhost:5000/.well-known/openid-configuration](http://localhost:5000/.well-known/openid-configuration) - a well-known url that show the current configuration.
- [https://jwt.io](https://jwt.io) - use to decode the auth token got from the server to see its contents.
- [https://github.com/IdentityServer/IdentityServer4.Quickstart.UI](https://github.com/IdentityServer/IdentityServer4.Quickstart.UI) - a UI for identity server (it doesn't come with one out of the box).
