//CAB301 assessment 3 - 2022
//The specifications of MemberCollection ADT
using System;
using System.Collections.Generic;
using System.Text;
interface iMovieSystem
{
    void addMovie(Movie aMovie); // add a new movie to the system

    void addQuantity(IMovie aMovie, int quantity); //add quantity to an existing movie to the system

    void deleteDVDs(IMovie aMovie, int quantity); //remove some DVDs of a movie from the system

    // void Find (IMember member); // find all the member inside the collection
    void addMember(IMember aMember); //Register a new member to the system

    //void Delete(Member aMember); //delete a member from the system

    //void displayMemberInfo(Member aMember); //given a member, display all the information of that member


    //void displayRentingMembers(Movie aMovie); // display all members who are renting the given movie

    //void displayTopThree(); //Display top three most frequently borrowed movies by the members in the descending order by the number of their frequency.

}