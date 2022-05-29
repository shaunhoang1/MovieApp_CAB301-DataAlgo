//CAB301 assessment 3 - 2022
//The specifications of MemberCollection ADT
using System;
using System.Collections.Generic;
using System.Text;
interface iMovieSystem
{
    void addMovie(Movie aMovie); // add a new movie to the system

    void addQuantity(IMovie aMovie, int quantity); //add quantity to an existing movie to the system

    //void Delete(Movie aMovie); //delete movie from the system

    //void Delete(Movie aMovie, int quantity); //remove some DVDs of a movie from the system


    // void Find (IMember member); // find all the member inside the collection
    void addMember(IMember aMember); //add a new member to the system

    //void Delete(Member aMember); //delete a member from the system

    //void displayBorrowingMovies(Member aMember); //given a member, display all the movies that the member are currently hold


    //void displayMovies(string aMovieType); // display all the tools of a tool type selected by a member

    //void borrowTool(Member aMember, Movie aMovie); //a member borrows a tool from the tool library

    //void returnTool(Member aMember, Movie aMovie); //a member return a tool to the tool library

    //string[] listTools(Member aMember); //get a list of tools that are currently held by a given member

    //void displayTopTHree(); //Display top three most frequently borrowed tools by the members in the descending order by the number of times each tool has been borrowed.

}