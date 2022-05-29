// Phase 2
// An implementation of MovieCollection ADT
// 2022


using System;

//A class that models a node of a binary search tree
//An instance of this class is a node in a binary search tree 
public class BTreeNode
{
	private IMovie movie; // movie
	private BTreeNode lchild; // reference to its left child 
	private BTreeNode rchild; // reference to its right child

	public BTreeNode(IMovie movie)
	{
		this.movie = movie;
		lchild = null;
		rchild = null;
	}

	public IMovie Movie
	{
		get { return movie; }
		set { movie = value; }
	}

	public BTreeNode LChild
	{
		get { return lchild; }
		set { lchild = value; }
	}

	public BTreeNode RChild
	{
		get { return rchild; }
		set { rchild = value; }
	}
}

// invariant: no duplicates in this movie collection
public class MovieCollection : IMovieCollection
{
	private BTreeNode root; // movies are stored in a binary search tree and the root of the binary search tree is 'root' 
	private int count; // the number of (different) movies currently stored in this movie collection 



	// get the number of movies in this movie colllection 
	// pre-condition: nil
	// post-condition: return the number of movies in this movie collection and this movie collection remains unchanged
	public int Number { get { return count; } }

	// constructor - create an object of MovieCollection object
	public MovieCollection()
	{
		root = null;
		count = 0;	
	}

	// Check if this movie collection is empty
	// Pre-condition: nil
	// Post-condition: return true if this movie collection is empty; otherwise, return false.
	public bool IsEmpty()
	{
		//To be completed
		return count == 0;
	}

	// Insert a movie into this movie collection
	// Pre-condition: nil
	// Post-condition: the movie has been added into this movie collection and return true, if the movie is not in this movie collection; otherwise, the movie has not been added into this movie collection and return false.
	public bool Insert(IMovie movie)
	{
		//To be completed
		Movie movies = (Movie)movie;

		if(root == null)
        {
			//Assign the movie to root if root is null
			root = new BTreeNode(movies);
			count++;
			return true;
        }
		else
        {
			//Assign the movie to different node if root is not null
			InsertBST(movies, root);
			return false;
        }
	}

	//Private method for Insert
	//Implementing Binary Search Tree Insert method
	private void InsertBST(Movie movie, BTreeNode ptr)
    {
		if(movie.CompareTo(ptr.Movie) < 0)
        {
			if(ptr.LChild == null)
            {
				//If left child of root is null, assign movie to left child
				ptr.LChild = new BTreeNode(movie);
				count++;
            }
            else 
			{
				//Assign the movie to different node if left child of root is not null
				InsertBST(movie, ptr.LChild);
			}
        }
        else
        {
			if(ptr.RChild == null)
            {
				//If right child of root is null, assign movie to right child
				ptr.RChild = new BTreeNode(movie);
				count++;
            }
            else
            {
				//Assign the movie to different node if right child of root is not null
				InsertBST(movie, ptr.RChild);
            }
        }
    }



	// Delete a movie from this movie collection
	// Pre-condition: nil
	// Post-condition: the movie is removed out of this movie collection and return true, if it is in this movie collection; return false, if it is not in this movie collection
	public bool Delete(IMovie movie)
	{
		//To be completed
		return DeleteBST(movie);
	}

	private bool DeleteBST(IMovie movie)
    {
		BTreeNode ptr = root; //Search reference
		BTreeNode parent = null; //Parent of ptr
		while((ptr != null) && (movie.CompareTo(ptr.Movie) != 0))
        {
			parent = ptr;
			//If movie is less that other movies in dictionary order
			if (movie.CompareTo(ptr.Movie) < 0) 
            {
				
				ptr = ptr.LChild; 
            }
            else
            {
				ptr = ptr.RChild;
            }
        }

		//
		if(ptr != null)
        {
			//if node has 2 children
			if ((ptr.LChild != null) && (ptr.RChild != null))
			{
				//if right subtree of ptr.LChild is null
				if (ptr.LChild.RChild == null)
				{
					ptr.Movie = ptr.LChild.Movie;
					ptr.LChild = ptr.LChild.LChild;
					count--;
					return true;
				}
				else
				{
					//find the node with highest value in left subtree of ptr
					BTreeNode p = ptr.LChild;
					BTreeNode pp = ptr;// parent of p
					while (p.RChild != null)
					{
						pp = p;
						p = p.RChild;
					}
					//copy the item at p to ptr
					ptr.Movie = p.Movie;
					pp.RChild = p.LChild;
					count--;
					return true;
				}
			}
            else
			//if node either has 1 child or is a leaf
            {
				BTreeNode c;
				if(ptr.LChild != null)
                {
					c = ptr.LChild;
                }
                else
                {
					c = ptr.RChild;
                }

				if(ptr == root)
                {
					root = c;
					count--;
					return true;
                }
                else
                {
					if(ptr == parent.LChild)
                    {
						parent.LChild = c;
                    }
                    else
                    {
						parent.RChild = c;
                    }
					count--;
					return true;
				}
            }
        }
		return false;
    }

    // Search for a movie in this movie collection
    // pre: nil
    // post: return true if the movie is in this movie collection;
    //	     otherwise, return false.
    public bool Search(IMovie movie)
    {
        //To be completed
        return SearchBST(movie, root);
    }

    private bool SearchBST(IMovie movie, BTreeNode root)
    {
		if(root != null)
        {
			//return true if Movie is found
            if(movie.CompareTo(root.Movie) == 0)
            {
				return true;
            }
            else
            {	
				//move down to the left child if movie title is lesser than root's value
				if(movie.CompareTo(root.Movie) < 0)
                {
					return SearchBST(movie, root.LChild);
                }
                else
                {
					//move down to the right child if movie title is greater than root's value
					return SearchBST(movie, root.RChild);
                }	
            }
        }
        else
        {
			//return false if movie is not found
			return false;
        }
    }

	// Search for a movie by its title in this movie collection  
	// pre: nil
	// post: return the reference of the movie object if the movie is in this movie collection;
	//	     otherwise, return null.
	public IMovie Search(string movietitle)
	{
        //To be completed
        return BSTSearchbyTitle(movietitle, root);
    }

	private IMovie BSTSearchbyTitle(string movietitle, BTreeNode root)
	{
		if (root != null)
		{
			if (movietitle.CompareTo(root.Movie.Title) == 0)
			{
				return root.Movie;
			}
			else
			{
				if (movietitle.CompareTo(root.Movie.Title) < 0)
				{
					return BSTSearchbyTitle(movietitle, root.LChild);
				}
				else
				{
					return BSTSearchbyTitle(movietitle, root.RChild);
				}
			}
		}
		else
		{
			return null;
		}
	}



	// Store all the movies in this movie collection in an array in the dictionary order by their titles
	// Pre-condition: nil
	// Post-condition: return an array of movies that are stored in dictionary order by their titles
	public IMovie[] ToArray()
    {
        //To be completed
        IMovie[] MovieList = new IMovie[count];
		int index = 0;
		InOrderTraversal(root, MovieList, ref index);

        return MovieList;
    }

    private void InOrderTraversal(BTreeNode root, IMovie[] MovieList, ref int index)
    {
		//Implementing In-Order Traversal in Binary Search Tree
		if (root != null)
        {
			InOrderTraversal(root.LChild, MovieList, ref index);
			MovieList[index] = root.Movie;
			index++;
			InOrderTraversal(root.RChild, MovieList, ref index);
		}
    }




    // Clear this movie collection
    // Pre-condotion: nil
    // Post-condition: all the movies have been removed from this movie collection 
    public void Clear()
	{
		//To be completed
		root = null;
		count = 0;
	}
}





