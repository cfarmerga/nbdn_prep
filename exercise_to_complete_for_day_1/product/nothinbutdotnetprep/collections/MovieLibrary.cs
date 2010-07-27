using System;
using System.Collections.Generic;

namespace nothinbutdotnetprep.collections
{
    public class MovieLibrary
    {
        IList<Movie> movies;

        public MovieLibrary(IList<Movie> list_of_movies)
        {
            this.movies = list_of_movies;
        }

        public IEnumerable<Movie> all_movies()
        {
            return new LazyEnumerable<Movie>(movies);
        }

        public void add(Movie movie)
        {
            if (already_contains(movie)) return;

            movies.Add(movie);
        }

        bool already_contains(Movie movie)
        {
            return movies.Contains(movie);
        }

        public IEnumerable<Movie> sort_all_movies_by_title_descending
        {
            get { return get_sorted(new AllMoviesByTitleDescending()); }
        }

        public IEnumerable<Movie> sort_all_movies_by_title_ascending
        {
            get { return get_sorted(new AllMoviesByTitleAscending()); }
        }

        public IEnumerable<Movie> sort_all_movies_by_movie_studio_and_year_published()
        {
            return get_sorted(new AllMoviesByMovieStudioAndYearPublished());
        }

        IEnumerable<Movie> get_sorted(IComparer<Movie> comparer)
        {
            var sorted = (List<Movie>) movies;
            sorted.Sort(comparer);

            foreach (var movie in sorted)
            {
                yield return movie;
            }
        }

        public IEnumerable<Movie> sort_all_movies_by_date_published_descending()
        {
            return get_sorted(new all_movies_by_date_published_descending());
        }

        public IEnumerable<Movie> sort_all_movies_by_date_published_ascending()
        {
            return get_sorted(new all_movies_by_date_published_ascending());
        }
    }

    public class AllMoviesByTitleAscending : IComparer<Movie>
    {
        public int Compare(Movie x, Movie y)
        {
            return x.title.CompareTo(y.title);
        }
    }

    public class AllMoviesByTitleDescending : IComparer<Movie>
    {
        public int Compare(Movie x, Movie y)
        {
            return y.title.CompareTo(x.title);
        }
    }

    public class AllMoviesByMovieStudioAndYearPublished : IComparer<Movie>
    {
        static Dictionary<ProductionStudio, int> rankings
            = new Dictionary<ProductionStudio, int>
            {
                {ProductionStudio.MGM, 1},
                {ProductionStudio.Pixar, 2},
                {ProductionStudio.Dreamworks, 3},
                {ProductionStudio.Universal, 4},
                {ProductionStudio.Disney, 5},
                {ProductionStudio.Paramount, 6},
            };

        public int Compare(Movie x, Movie y)
        {
            var rankX = (GetRank(x)*10000) + x.date_published.Year;
            var rankY = (GetRank(y)*10000) + y.date_published.Year;

            return rankX.CompareTo(rankY);
        }

        int GetRank(Movie x)
        {
            return rankings.ContainsKey(x.production_studio)
                ? rankings[x.production_studio]
                : 1000;
        }
    }

    public class all_movies_by_date_published_ascending : IComparer<Movie>
    {
        public int Compare(Movie x, Movie y)
        {
            return x.date_published.CompareTo(y.date_published);
        }
    }

    public class all_movies_by_date_published_descending : IComparer<Movie>
    {
        public int Compare(Movie x, Movie y)
        {
            return y.date_published.CompareTo(x.date_published);
        }
    }
}