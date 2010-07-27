using System;
using nothinbutdotnetprep.utility;
using nothinbutdotnetprep.utility.filtering;

namespace nothinbutdotnetprep.collections
{
    public class Movie  : IEquatable<Movie>
    {
        public string title { get; set; }
        public ProductionStudio production_studio { get; set; }
        public Genre genre { get; set; }
        public int rating { get; set; }
        public DateTime date_published { get; set; }

        public bool Equals(Movie other)
        {
            if (other == null) return false;

            return ReferenceEquals(this,other) || 
                is_equal_to_non_null_instance_of(other);
        }

        bool is_equal_to_non_null_instance_of(Movie other)
        {
            return this.title == other.title;
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as Movie);
        }

        public override string ToString()
        {
            return title;
        }

        public void do_something()
        {

        }

        public static Criteria<Movie> is_published_by_pixar_or_disney
        {
            get
            {
                return Where<Movie>.has_a(x => x.production_studio)
                    .equal_to_any(ProductionStudio.Pixar, ProductionStudio.Disney);
            }
        }


        public static Criteria<Movie> is_published_after_year(int year)
        {
            return new AnonymousCriteria<Movie>(item => item.date_published.Year > year);
        }

        public static Criteria<Movie> is_published_within_year_range(int startYear, int endYear)
        {
            return
                new AnonymousCriteria<Movie>(
                    item => item.date_published.Year >= startYear && item.date_published.Year <= endYear);
        }

        public static Criteria<Movie> is_of_genre(Genre genre)
        {
            return new AnonymousCriteria<Movie>(item => item.genre == genre);
        }
    }
}