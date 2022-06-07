
public class MovieSystem: iMovieSystem
{
    const int staff_login = 1, member_login = 2, exit_program = 0;
    const int selection1 = 1, selection2 = 2, selection3 = 3,
            selection4 = 4, selection5 = 5, selection6 = 6, selection0 = 0;

    Movie StarWars = new("Starwars", MovieGenre.Action, MovieClassification.G, 90, 5);
    Movie Loki = new("Loki", MovieGenre.Action, MovieClassification.M15Plus, 50, 8);
    Movie DrStrange = new("DrStrange", MovieGenre.Action, MovieClassification.M15Plus, 50, 7);
    Movie Avengers = new("Avengers", MovieGenre.Action, MovieClassification.M15Plus, 50, 10);
    Movie IronMan = new("IronMan", MovieGenre.Action, MovieClassification.M15Plus, 120, 10);

    MemberCollection members = new MemberCollection(100);
    IMovieCollection movieList = new MovieCollection();
    IMember currentMember;
    IMember findMember;
    IMovie findMovie;
    
    
    public void addExistingMovietoSystem()
    {
        movieList.Insert(StarWars);
        movieList.Insert(Loki);
        movieList.Insert(DrStrange);
        movieList.Insert(Avengers);
        movieList.Insert(IronMan);
    }
    public void addMovie(Movie aMovie)
    {
        movieList.Insert(aMovie);
        Console.WriteLine("A new movie has been added to the system");
    }

    public void addQuantity(IMovie aMovie, int quantity)
    {
        if (aMovie.TotalCopies + quantity > 10)
        {
            Console.WriteLine("The total of DVDs exceed the maximum 10 DVDs of a movie with " + (aMovie.TotalCopies + quantity - 10) + " DVDs. Please try add smaller number");
        }
        else
        {
            aMovie.TotalCopies += quantity;
            aMovie.AvailableCopies += quantity;
            Console.WriteLine(quantity + " DVDs has been added to the " + aMovie.Title + ". Available Copies: " + aMovie.AvailableCopies + ". Total Copies: " + aMovie.TotalCopies);
        }
        
    }

    public void deleteDVDs(IMovie aMovie, int quantity) 
    {
        if (aMovie.AvailableCopies < quantity)
        {
            Console.WriteLine("The delete quantity is greater than the amount in the system. Pleasse try again");
        }
        else
        {
            aMovie.AvailableCopies -= quantity;
            aMovie.TotalCopies -= quantity;
            Console.WriteLine(quantity + " Dvds has been removed out of a movie");
            if (aMovie.TotalCopies == 0)
            {
                Console.WriteLine("There is no DVDs left, deleted the movie out of the system");
                movieList.Delete(aMovie);
            }
        }
    }

    public void addMember(IMember aMember)
    {
        members.Add(aMember);
    }

    public void Delete (IMember aMember)
    {
        members.Delete(aMember);
    }

    public void displayMemberPhoneNum(IMember aMember)
    {
        if (members.Search(aMember)) {
            IMember findMem = members.Find(aMember);
            Console.WriteLine("The contact phone number of " + findMem.FirstName.ToString() + " " + findMem.LastName.ToString() + " is: " + findMem.ContactNumber.ToString());
        }
    }

    public void displayRentingMembers(IMovie aMovie)
    {
        Console.WriteLine(aMovie.Borrowers.ToString());
    }

    public void displayMovieInfo(string movieTitle)
    {
        findMovie = movieList.Search(movieTitle);
        if (findMovie == null)
        {
            Console.WriteLine("The movie does not exist, please enter the correct title");
        } else
        {
            Console.WriteLine(findMovie.ToString());
        }
    }

    public void borrowMovie(IMovie aMovie)
    {
        if (aMovie.AddBorrower(currentMember))
        {
            aMovie.AddBorrower(currentMember);
            Console.WriteLine("You have borrowed " + aMovie.Title + " successfully");
        } else
        {
            Console.WriteLine("You can only borrow 1 DVDs each movie. Please try again");
        }
    }

    public void returnMovie(IMovie aMovie)
    {
        aMovie.RemoveBorrower(currentMember);
        Console.WriteLine("You have return " + aMovie.Title + " successfully");
    }

    public void moviesBorrowList()
    {
        IMovie[] movieBorrowList = movieList.ToArray();
        for (int i = 0; i < movieBorrowList.Length; i++)
        {
            if (movieBorrowList[i].Borrowers.Search(currentMember))
            {
                Console.WriteLine(movieBorrowList[i].ToString());
            } else
            {
                Console.WriteLine("You did not borrow any movie yet.");
                break;
            }
        }
        Console.WriteLine();
    }

    public void displayTop3Movies()
    {
        IMovie[] movieArray = movieList.ToArray(); 
        int first, second, third;
        IMovie firstMovie = null, secondMovie = null, thirdMovie = null;
        first = second = third = int.MaxValue;

        if (movieArray.Length < 3)
        {
            Console.WriteLine("Invalid Input");
        }
        else
        {
            for (int i = 0; i < movieArray.Length; i++)
            {
                if (movieArray[i].AvailableCopies < first)
                {
                    third = second;
                    second = first;
                    first = movieArray[i].AvailableCopies;

                    thirdMovie = secondMovie;
                    secondMovie = firstMovie;
                    firstMovie = movieArray[i];
                }
                else if (movieArray[i].AvailableCopies < second)
                {
                    third = second;
                    second = movieArray[i].AvailableCopies;

                    thirdMovie = secondMovie;
                    secondMovie = movieArray[i];
                }
                else if (movieArray[i].AvailableCopies < third)
                {
                    third = movieArray[i].AvailableCopies;
                    thirdMovie = movieArray[i];
                }
            }
        }

        Console.WriteLine("#1: {0}\n#2: {1}\n#3: {2}\n", firstMovie.ToString(), secondMovie.ToString(), thirdMovie.ToString());
    }

    public void MainMenu()
    {
        ////////////////////////////// Assessment Task 3 User Interface////////////////////////////////////

        /////////////////////////////////////////Main Menu////////////////////////////////////////////////////////////////////////
        while (true)
        {
            Console.WriteLine("==============================================================");
            Console.WriteLine("Welcome to Community Library Movie DVD Management System");
            Console.WriteLine("==============================================================");

            Console.WriteLine("=======================Main Menu==============================");

            Console.WriteLine("1. Staff Login\n2. Member Login\n0. Exit");
            Console.WriteLine("Enter your choice ==> (1/2/0)");
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
                    Console.WriteLine("Login Successfully as Staff");
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
                Console.WriteLine("Enter your First name: ");
                string firstName = Console.ReadLine();
                Console.WriteLine("Enter your Last name: ");
                string lastName = Console.ReadLine();
                IMember searchMember =  new Member(firstName, lastName);
                findMember = members.Find(searchMember);
                //Check the account
                if (members.Search(searchMember)) // if user exist
                {
                    Console.WriteLine("Enter your Password: ");
                    password = Console.ReadLine();
                    if (password == findMember.Pin)
                    {
                        currentMember = findMember;
                        Console.WriteLine("Login Successfully as User. Hello " + currentMember.FirstName + " " + currentMember.LastName);
                        MemberLogin();
                    } else
                    {
                        Console.WriteLine("Incorrect Password... Please try again");
                    }
                } else
                {
                    Console.WriteLine("Incorrect Account Name... Please try again");
                    break;
                }  
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
            Console.WriteLine("0. Return to the main menu");
            Console.WriteLine("Enter your choice ==> (1/2/3/4/5/6/0)");

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
                findMovie = movieList.Search(movie);
                if (findMovie != null)
                {
                    if(findMovie.TotalCopies == 10)
                    {
                        Console.WriteLine("The movie exist in the system. But total copies is reach the limit. You can't add more DVDs");
                        break;
                    }
                    Console.WriteLine("The movie exist in the system. You just need add on quantity");
                    Console.WriteLine("Please choose the quantity of DVDs you want to add to the movie: ");
                    string enterQuantity = Console.ReadLine();
                    int quantityInput = InputValidation(enterQuantity);
                    addQuantity(movieList.Search(movie), quantityInput);
                } else
                {
                    // check if the movie is not existing in the library
                    Console.WriteLine("The movie does not exist in the system");
                    Console.WriteLine("Add new dvds of a movie");
                    Console.WriteLine("Enter movie name: ");
                    string movieName = Console.ReadLine();
                    Console.WriteLine("List of options for genre: ");
                    Console.WriteLine("1: Action");
                    Console.WriteLine("2: Comedy");
                    Console.WriteLine("3: History");
                    Console.WriteLine("4: Drama");
                    Console.WriteLine("5: Western");
                    Console.WriteLine("Enter movie genre: ");
                    string enterGenre = Console.ReadLine();
                    if (Convert.ToInt32(enterGenre) < 1 || Convert.ToInt32(enterGenre) > 5) // Check if the input out of option
                    {
                        Console.WriteLine("Invalid genre. Please try again with the option from 1-5");
                        break;
                    }
                    int movieGenre = InputValidation(enterGenre);
                    if (movieGenre == -1) // Check if input is invalid
                    {
                        Console.WriteLine("Invalid genre. Please try again");
                        break;
                    }
                    Console.WriteLine("List of options for classification: ");
                    Console.WriteLine("1: G");
                    Console.WriteLine("2: PG");
                    Console.WriteLine("3: M");
                    Console.WriteLine("4: M15Plus");
                    Console.WriteLine("Enter movie classification: ");
                    string enterClassification = Console.ReadLine();
                    if (Convert.ToInt32(enterClassification) < 1 || Convert.ToInt32(enterClassification) > 4) // Check if the input out of option
                    {
                        Console.WriteLine("Invalid classification. Please try again with the option from 1-4");
                        break;
                    }
                    int movieClassification = InputValidation(enterClassification);
                    if (movieClassification == -1)
                    {
                        Console.WriteLine("Invalid classification. Please try again");
                        break;
                    }
                    Console.WriteLine("Enter movie duration: ");
                    string enterDuration = Console.ReadLine();
                    int movieDuration = InputValidation(enterDuration);
                    if (movieDuration == -1)
                    {
                        Console.WriteLine("Invalid Duration. Please try again");
                        break;
                    }
                    Console.WriteLine("Enter movie quantity: ");
                    string enterQuantity = Console.ReadLine();
                    int quantity = InputValidation(enterQuantity);
                    if (quantity == -1)
                    {
                        Console.WriteLine("Invalid Quantity. Please try again");
                        break;
                    }
                    if (quantity > 10)
                    {
                        Console.WriteLine("The maximum quantity for each movie in the system is 10. Please try again");
                        break;
                    }
                    Movie newMovie = new Movie(movieName, (MovieGenre)movieGenre, (MovieClassification)movieClassification, movieDuration, quantity);
                    addMovie(newMovie);
                    Console.WriteLine(newMovie.ToString());
                }
                
                break;


            case selection2:
                IMovie[] printMovieList = movieList.ToArray();
                for (int i = 0; i < printMovieList.Length; i++)
                {
                    Console.WriteLine(printMovieList[i].ToString());
                }
                Console.WriteLine("Please choose a movie you want to remove DVDs: ");
                string movieNameDelete = Console.ReadLine();
                if (movieList.Search(movieNameDelete) == null)
                {
                    Console.WriteLine("The movie is not inside the system. Please see the list above to choose correctly");
                    break;
                }
                Console.WriteLine("Please choose the quantity of DVDs you want to delete from the movie: ");
                string enterDvdQuantity = Console.ReadLine();
                int quantityDvdInput = InputValidation(enterDvdQuantity);
                if (quantityDvdInput == -1)
                {
                    Console.WriteLine("Invalid Quantity. Please try again");
                    break;
                }
                deleteDVDs(movieList.Search(movieNameDelete), quantityDvdInput);
                break;

            case selection3:
                Console.WriteLine("Register a new member");
                Console.WriteLine("Please enter user's First Name: ");
                string enterFirstName = Console.ReadLine();
                Console.WriteLine("Please enter user's Last Name: ");
                string enterLastName = Console.ReadLine();
                Console.WriteLine("Please enter user's phone number: ");
                string enterPhoneNumber = Console.ReadLine();
                if(!IMember.IsValidContactNumber(enterPhoneNumber))
                {
                    Console.WriteLine("Invalid Phone Number. It must contain 10 digits start with digit 0. Please try again");
                    break;
                }
                Console.WriteLine("Please enter user's pin/password: ");
                string enterPassword = Console.ReadLine();
                if (!IMember.IsValidPin(enterPassword))
                {
                    Console.WriteLine("Invalid pin. It must contain 4-6 digits. Please try again");
                    break;
                }
                Member newMember = new Member(enterFirstName, enterLastName, enterPhoneNumber, enterPassword);
                addMember(newMember);
                Console.WriteLine("Register successfully"); 
                Console.WriteLine(members.ToString());
                break;

            case selection4:
                Console.WriteLine("Remove a registered member");
                Console.WriteLine("Please enter delete First Name: ");
                string deleteFirstName = Console.ReadLine();
                Console.WriteLine("Please enter delete Last Name: ");
                string deleteLastName = Console.ReadLine();
                Member deleteUser = new Member(deleteFirstName, deleteLastName);
                if (members.Search(deleteUser))
                {
                    Delete(deleteUser);
                    Console.WriteLine(deleteUser.FirstName + " " + deleteUser.LastName + " has been deleted");
                } 
                else
                {
                    Console.WriteLine("The user does not exist. Please try again");
                    break;
                }
                
                
                break;

            case selection5:
                Console.WriteLine("Display a member's phone number");
                Console.WriteLine("Please enter member First Name: ");
                string firstName5 = Console.ReadLine();
                Console.WriteLine("Please enter member Last Name: ");
                string lastName5 = Console.ReadLine();
                Member mem5 = new Member(firstName5, lastName5);
                if (members.Search(mem5))
                {
                    displayMemberPhoneNum(mem5);
                }
                else
                {
                    Console.WriteLine("The user does not exist. Please try again");
                    break;
                }
                break;

            case selection6:
                Console.WriteLine("Display all members who are currently renting a movie");
                Console.WriteLine("Enter the movie you want to display borrowers");
                string enterMovie = Console.ReadLine();
                if (movieList.Search(enterMovie) != null)
                {
                    findMovie = movieList.Search(enterMovie);
                    if (findMovie.Borrowers.Number == 0)
                    {
                        Console.WriteLine("This movie has no borrower");
                        break;
                    }
                    displayRentingMembers(findMovie);
                } else
                {
                    Console.WriteLine("Could not find the movie. Please try again");
                    break;
                }
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
            Console.WriteLine("5. List current borrowing");
            Console.WriteLine("6. Display the top 3 movies rented by the members");
            Console.WriteLine("0. Return to the main menu");
            Console.WriteLine("Enter your choice ==> (1/2/3/4/5/6/0)");

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
                IMovie[] printMovieList = movieList.ToArray();
                for (int i = 0; i < printMovieList.Length; i++)
                {
                    Console.WriteLine(printMovieList[i].ToString());
                }
                break;

            case selection2:
                Console.WriteLine("Display movie info");
                Console.Write("Enter the movie title you want to see the information: ");
                string enterTitle = Console.ReadLine();
                displayMovieInfo(enterTitle);
                break;

            case selection3:
                Console.WriteLine("Borrow movie");
                Console.Write("Enter the movie you want to borrow: ");
                string borMovie = Console.ReadLine();
                findMovie = movieList.Search(borMovie);
                if (findMovie != null)
                {
                    borrowMovie(findMovie);
                } else
                {
                    Console.WriteLine("There is no movie like this in our system. Please try again");
                }
                break;

            case selection4:
                Console.WriteLine("Return a movie DVD");
                Console.Write("Enter the movie you want to return: ");
                string enterReturnMovie = Console.ReadLine();
                findMovie = movieList.Search(enterReturnMovie); 
                if (findMovie != null)
                {
                    if (findMovie.Borrowers != currentMember)
                    {
                        Console.WriteLine("You have not borrow this movie. Please try again");
                        break;
                    }
                    returnMovie(findMovie);
                } else
                {
                    Console.WriteLine("There is no movie like this in your borrow list. Please try again");
                }
                break;

            case selection5:
                Console.WriteLine("List current borrowings");
                moviesBorrowList();
                break;

            case selection6:
                Console.WriteLine("Display top 3 movies");
                displayTop3Movies();
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

    /////////////////////////////////////////////Helper Functions for displaying top 3 movies //////////////////////////////////////////////////////////////
    public static int FindLargestElements(int[] array)
    {
        int first, second, third;
        int count = 0;
        first = second = third = int.MinValue;

        if (array.Length < 3)
        {
            Console.WriteLine("Invalid Input");
        }
        else
        {
            for (int i = 0; i < array.Length; i++)
            {
                count++;
                if (array[i] > first)
                {
                    third = second;
                    second = first;
                    first = array[i];
                }
                else if (array[i] > second)
                {
                    third = second;
                    second = array[i];
                }
                else if (array[i] > third)
                {
                    third = array[i];
                    count++;
                }
            }
        }
        return count;

    }
}