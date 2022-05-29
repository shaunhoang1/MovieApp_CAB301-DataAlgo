
public class MovieSystem: iMovieSystem
{
    const int staff_login = 1, member_login = 2, exit_program = 0;
    const int selection1 = 1, selection2 = 2, selection3 = 3,
            selection4 = 4, selection5 = 5, selection6 = 6, selection0 = 0;

    Movie StarWars = new("Starwars", MovieGenre.Action, MovieClassification.G, 90, 10);
    Movie Loki = new("Loki", MovieGenre.Action, MovieClassification.M15Plus, 50, 10);
    Movie DrStrange = new("DrStrange", MovieGenre.Action, MovieClassification.M15Plus, 50, 10);
    Movie Avengers = new("Avengers", MovieGenre.Action, MovieClassification.M15Plus, 50, 10);
    Movie IronMan = new("IronMan", MovieGenre.Action, MovieClassification.M15Plus, 120, 10);

    static MemberCollection users = new MemberCollection(100);
    IMovieCollection movieList = new MovieCollection();
    
    public void addExistingMovietoSystem()
    {
        movieList.Insert(StarWars);
        movieList.Insert(Loki);
        movieList.Insert(DrStrange);
        movieList.Insert(Avengers);
        movieList.Insert(IronMan);
        Console.WriteLine(Loki.ToString());
        movieList.ToArray();
        Console.WriteLine(movieList.Number);
    }
    

    public void addMovie(Movie aMovie)
    {
        movieList.Insert(aMovie);
        Console.WriteLine("A new movie has been added to the system");
    }

    public void addQuantity(IMovie aMovie, int quantity)
    {
        aMovie.TotalCopies += quantity;
        aMovie.AvailableCopies += quantity;
        Console.WriteLine(quantity + "DVDs has been added to the" + aMovie.Title);
    }

    public void addMember(IMember aMember)
    {
        users.Add(aMember);
    }

    public void MainMenu()
    {
        ////////////////////////////// Assessment Task 3 User Interface////////////////////////////////////

        /////////////////////////////////////////Main Menu////////////////////////////////////////////////////////////////////////
        while (true)
        {
            Console.WriteLine("==============================================================");
            Console.WriteLine("Welcome to Community Library Movie DBD Mangaement System");
            Console.WriteLine("==============================================================");

            Console.WriteLine("=======================Main Menu==============================");

            Console.WriteLine("1. Staff Login\n2. Member Login\n0. Exit");
            Console.WriteLine("Please enter your choice ==> (1/2/0)");
            Console.WriteLine(movieList.Number);
            string enterValue = Console.ReadLine();
            int optionInput = InputValidation(enterValue);
            
            if (optionInput == exit_program)
            {
                break;
            }
            else
            {
                ProcessSelection(optionInput);
            }
            

        }
    }

    /////////////////////////////////////////////Process option input for Main Menu///////////////////////////////////////////////////////////////
    public void ProcessSelection(int input)
    {
        switch (input)
        {
            case staff_login:
                //Enter account and password 
                Console.WriteLine("Enter your account name: ");
                string accountname = Console.ReadLine();
                Console.WriteLine("Enter your Password: ");
                string password = Console.ReadLine();
                //Check the account
                if (accountname == "staff" && password == "today123")
                {
                    StaffLogin();
                }
                else
                {
                    Console.WriteLine("Incorrect password or Account Name... Please try again");
                    break;
                }
                break;

            case member_login:
                //Enter account and password 
                Console.WriteLine("Enter your account name: ");
                accountname = Console.ReadLine();
                Console.WriteLine("Enter your Password: ");
                password = Console.ReadLine();
                // if (users.Search((Member)accountname))
                
                //Check the account
                MemberLogin();
                break;

            default:
                Console.WriteLine("Invalid Input. Please enter the one of the options above");
                break;
        }
    }

    ///////////////////////////////////////////////////Staff Menu///////////////////////////////////////////////////////////////////////////////
    public void StaffLogin()
    {
        while (true)
        {
            Console.WriteLine("=======================Staff Menu==============================");
            Console.WriteLine("1. Add new DVDs of a new movie to the system");
            Console.WriteLine("2. Remove DVDs of a movie from the system");
            Console.WriteLine("3. Register a new member with the system");
            Console.WriteLine("4. Remove a registered member from the system");
            Console.WriteLine("5. Display a member's contact phone number, given the member's name");
            Console.WriteLine("6. Display all members who are currently renting a praticular movie");
            Console.WriteLine("7. Return to the main menu");

            string enterValue = Console.ReadLine();
            int staffSelection = InputValidation(enterValue);

            if (staffSelection == selection0)
            {
                break;
            }
            else
            {
                ProcessStaffSelection(staffSelection);
            }
        }
    }


    /////////////////////////////////////////////Process option input for Staff Menu///////////////////////////////////////////////////////////////
    public void ProcessStaffSelection(int input)
    {
        switch (input)
        {
            case selection1:
                Console.WriteLine("Find if the movie is existing in the system: ");
                string movie = Console.ReadLine();
                // check if the movie is existing, then just need to add the quantity to the existing => Assume that the movie title is unique
                Console.WriteLine(movieList.Search(movie));
                if(true)
                    {
                    Console.WriteLine("The movie existing in the system");
                    Console.WriteLine("Please choose the quantity of DVDs you want to add to the movie: ");
                    string enterValue = Console.ReadLine();
                    int quantityInput = InputValidation(enterValue);
                    //if ((quantityInput + aMovie.TotalCopies) > 10)
                    //{
                    //    Console.WriteLine("The amount of DVDs exceed the limit by " + (quantityInput + aMovie.TotalCopies - 10) + "DVDs, please try again");
                    //}

                    addQuantity(movieList.Search(movie), quantityInput);
                } else
                {
                    // check if the movie is not existing in the library
                    Console.WriteLine("Add new dvds of a movie");
                    Console.WriteLine("Enter movie name: ");
                    string movieName = Console.ReadLine();
                    Console.WriteLine("Enter movie genre: ");
                    int movieGenre = InputValidation(Console.ReadLine());
                    Console.WriteLine("Enter movie classification: ");
                    int movieClassification = InputValidation(Console.ReadLine());
                    Console.WriteLine("Enter movie duration: ");
                    int movieDuration = InputValidation(Console.ReadLine());
                    Console.WriteLine("Enter movie quantity: ");
                    int quantity = InputValidation(Console.ReadLine());
                    Movie newMovie = new Movie(movieName, (MovieGenre)movieGenre, (MovieClassification)movieClassification, movieDuration, quantity);
                    addMovie(newMovie);
                }
                
                break;


            case selection2:
                // case remove entire movie out of the library
                Console.WriteLine("Movie Title has been removed out of the system");

                // case remove quantity of dvds of movie out of the library
                // if there is no quantity left, remove entire movie
                Console.WriteLine("Quantity of dvds has been removed out of a movie");
                break;

            case selection3:
                // Save member's firstname, lastname, contact phone number
                Console.WriteLine("Register a new member");
                break;

            case selection4:
                Console.WriteLine("Remove a registered member");
                break;

            case selection5:
                Console.WriteLine("Display a member's phone number");
                break;

            case selection6:
                Console.WriteLine("Display all members who are currently renting a movie");
                break;

            default:
                Console.WriteLine("Invalid Input. Please enter the one of the options above");
                break;
        }
    }

    ///////////////////////////////////////////////////Member Menu///////////////////////////////////////////////////////////////////////////////
    public void MemberLogin()
    {
        while (true)
        {
            Console.WriteLine("=======================Member Menu==============================");
            Console.WriteLine("1. Browse all the movies");
            Console.WriteLine("2. Display all the information about a movie, given the title of the movie");
            Console.WriteLine("3. Borrow a movie DVD");
            Console.WriteLine("4. Return a movie DVD");
            Console.WriteLine("5. List current bow");
            Console.WriteLine("6. Display the top 3 movies rented by the members");
            Console.WriteLine("0. Return to the main menu");

            //int memberSelection = Convert.ToInt32(Console.ReadLine());
            string enterValue = Console.ReadLine();
            int memberSelection = InputValidation(enterValue);

            if (memberSelection == selection0)
            {
                break;
            }
            else
            {
                ProcessMemberSelection(memberSelection);
            }
        }
    }
    /////////////////////////////////////////////Process option input for Member Menu///////////////////////////////////////////////////////////////
    public void ProcessMemberSelection(int input)
    {
        switch (input)
        {
            case selection1:
                Console.WriteLine("Browse movie");
                break;

            case selection2:
                Console.WriteLine("Display movie info");
                break;

            case selection3:
                Console.WriteLine("Borrow movie");
                break;

            case selection4:
                Console.WriteLine("Return a movie DVD");
                break;

            case selection5:
                Console.WriteLine("List current borrowings");
                break;

            case selection6:
                Console.WriteLine("Display top 3 movies");
                break;

            default:
                Console.WriteLine("Input not in range. Please enter the one of the options above");
                break;
        }
    }

    ////////////////////////////////////////////Valid option input for both member and staff menu///////////////////////////////////////////////////////////
    public static int InputValidation(string input)
    {
        int valid = 0;

        for (int i = 0; i < input.Length; i++)
        {
            if (input[i] >= '0' && input[i] <= '9')
            {
                valid++;
            }
        }

        if (valid == input.Length)
        {
            int value = Convert.ToInt32(input);
            return value;
        }
        else
        {
            Console.WriteLine("Invald Input. Please enter a numeric value");
        }

        return -1;
    }
}