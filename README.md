<h1>FictionHoarder</h1>
<p>
A desktop app that allows the hoarding and reading of fiction. You'll be able to get stories from the web and archive them in your personal library.

This app will allow users to gather and store online books into a personal library. 
</p>

<h2>Finishing what I started</h2>
<p>
This is a project I started earlier this year but left unfinished and untouched for months. At the time I had no experience with WPF and was simply coding for the sake of coding. When my lack of experience caught up with me I finally decided to put this project on hold. Months later, after taking online courses for WPF develpoment and working on other smaller projects I have finally returned to finish what I started.
</p>

<h2>Choosing my Data Access with Dapper</h2>
<p>
When returning to this project I was contemplating using Entity Framework Core, as this acted as my data access for my previous projects and I was already familiar with its use. At the time it seemed like a no brainer but then I came across the Dapper ORM and its more hands-on approach with SQL queries. I had very little experience with T-SQL at this point and figured that this would be a great opportunity to familiarize myself with its various concepts.

I manually created a database in Visual Studio and used stored procedures that would be accessed by a seperate project that acted as a class library. The class library would be accessed by the API and used to return data to various API endpoints. 
  
This project works with many-to-many relationships and all of the stories' file paths are held in cloud storage using Google's Firebase storage. 
</p>

<h2>Structure</h2>
<p>
This project is made up of 5 seperate projects that deal with Front-End and Back-End concerns. FictionHoarderWPF and FictionUI_Library deal with all Front-End/UI concerns. FictionUI_Library communicates with the API endpoints and sends data to FictionHoarderWPF. FictionHoarderWPF recieves and displays data for the end user.
  
The Back-End is made up of FictionAPI, FictionDataAccessLibrary, and FictionDb. FictionAPI uses the Repository Pattern to create a seperation of concerns and prevent messy code from propagating throughout our controller endpoints.
</p>
<img src="https://user-images.githubusercontent.com/88408654/207149073-b30270d0-4e4c-4e32-b3ba-d7d12df93f31.PNG"/>

<h2>Not overly complicated</h2>
<p>
I'm not exactly a UI/UX designer so the design of the app isn't anything special. The UI consists of a MainView that is capable of displaying 8 other views:

  <ul>
    <li>Start Up View</li>
    <li>Home Page View</li>
    <li>Reading View</li>
    <li>Home view</li>
    <li>Search View</li>
    <li>Stories View</li>
    <li>History View</li>
    <li>Account View</li>
  </ul>
  
  <h3>Start Up</h3>
  <p>
  This is where the user can login or register. The Authorization for this app is rather simple. The password has one layer of encryption and is done with the FictionAPI project under the Data/AuthRepository file. The password is made up of a passwordhash and passwordsalt.
  </p>
  <img src="https://user-images.githubusercontent.com/88408654/207156220-720366c3-e990-4541-b6bd-cb34988d4f00.PNG"/>
  <img src="https://user-images.githubusercontent.com/88408654/207156388-4050f0b2-ce9b-4e42-b606-ae71589ee948.PNG"/>
  
  <h3>Home Page</h3>
  <p>
  The home page view allows the user to access the Home, Search, Stories, History, and Account views.
  </p>
  <img src="https://user-images.githubusercontent.com/88408654/207158307-1fde4791-2a19-4aa2-95e4-9cd9b7021505.PNG"/>
 
  <h3>Home</h3>
  <p>
  The home view welcomes the user and displays the 4 most recent stories viewed.
  </p>
  <img src="https://user-images.githubusercontent.com/88408654/207158632-8dfccea3-c67e-429d-85c8-91b09df65125.PNG"/>
  
  <h3>Search</h3>
  <p>
  The search view allows the user to search their computer for epub files to add to their library.
  </p>
  <img src="https://user-images.githubusercontent.com/88408654/207160021-e2f42e4d-2801-4007-a7ee-5aea4ab68448.PNG"/>
  
  <h3>Stories</h3>
  <p>
  The stories view allows the user to see their stories and choose which story they want to read.
  </p>
  <img src="https://user-images.githubusercontent.com/88408654/207161032-8b0db784-736c-448c-a811-836b69c793d3.PNG"/>
  
  <h3>History</h3>
  <p>
  The history view much like the stories view displays stories, but the stories in this view are the stories that have been read. They are ordered from the latest story read.
  </p>
  <img src="https://user-images.githubusercontent.com/88408654/207161015-3c0e24d7-c8da-4eb8-9ca0-171b863799e9.PNG"/>
  
  <h3>Account</h3>
  <p>
  The account view allows the user to see their account info and edit it. They can also choose to log out.
  </p>
  <img src="https://user-images.githubusercontent.com/88408654/207161004-92b4c078-61a7-4bb0-8394-0629d73c6eef.PNG"/>
  
  <h3>Reading</h3>
  <p>
  The reading view displays the content of the chosen story. Using EpubSharp(https://github.com/asido/EpubSharp) to retrieve the html files from each story then converting them into xaml with the HtmlToXamlConvert by Nathan Harrenstein. This xaml file is then used to create a flow document to display the story.
  </p>
  
  <img src="https://user-images.githubusercontent.com/88408654/207163047-4307f06f-9c3b-4c89-8d38-a3124ed6bac3.PNG">
  
  <p>
  As shown above, this solution isn't perfect. When planning out this app I was mainly concerned with epub files that were generated from fanfiction and not from original published stories, like 'Alice's Adventures in Wonderland'. Because of this the chapters don't conincide correctly with stories that are not fanfiction. They also don't look the best when turned into flow documents.
  </p>
  
</p>

<h2>The End</h2>
<p>
Not perfect, but it works. I could probably optimize a few things and make the app prettier but I wanted a finished and functional project, which is what I currently have. I am happy to say that I can call it a day and begin working on something else, besides I can always come back to this later if I want to. Time to get reading!
</p>
