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

The project specifications above are fairly simple, but, a number of features were included to showcase some of the architectural considerations and use of inversion-of-control.

A pure MVC solution was preferred to using AJAX and leaning more heavily on jQuery due to the nature of the project and the requirements.  This is intended to display a proficiency with ASP.NET MVC.  View models are preferred over the ViewData dictionary, but some vestiges of the scaffolded boilerplate remain such as the use of ViewData["Title"] in Razor views (.cshtml files).

Data is accessed as JSON via HTTP using a read-only wrapper around an HTTP client.  This is then implemented as generic repository methods which are, in turn, wrapped within business services.  Models were created within the domain separately from the native entities exposed by the JSON repository.  This was done to highlight entity-mapping features and functionality.  The HTTP client objects themselves are tied to specific classes by use of generic types in order to facilitate better dependency injection- namely, the URL strings passed to the client constructors.

Many additional service layer methods have been provided than are implemented.  This is due to a natural progression of some of the business process design and a preference to have some functionality available in the case of future requests.

Thanks to Steve Gordon for his post on registering generics using the Dot.NET Core IoC container: https://www.stevejgordon.co.uk/asp-net-core-dependency-injection-how-to-register-generic-types.
