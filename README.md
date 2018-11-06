# mvc-album-browser
### ASP.NET MVC project to display album, user, and post information

Created in Dot.NET Core 1.1.  Wraps datasets at https://jsonplaceholder.typicode.com.

Displays a paged set of albums with search capabilities for album title and user's name.

Information displayed includes:
  * First album thumbnail
  * Album title
  * User name
  * User email address
  * User phone number
  * User address

Navigating via album title displays all associated thumbnails for the selected album and the album title below the thumbnails.

Navigating via user name displays all detail for the selected user and all associated posts for the user.

Thanks to Steve Gordon for his post on registering generics using the Dot.NET Core IoC container: https://www.stevejgordon.co.uk/asp-net-core-dependency-injection-how-to-register-generic-types.
