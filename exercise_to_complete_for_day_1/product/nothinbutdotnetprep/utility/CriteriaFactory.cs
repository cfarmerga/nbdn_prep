﻿namespace nothinbutdotnetprep.utility
{
    public interface CriteriaFactory<ItemToFilter, PropertyType>
    {
        Criteria<ItemToFilter> equal_to(PropertyType value_to_match);
        Criteria<ItemToFilter> equal_to_any(params PropertyType[] values);
        Criteria<ItemToFilter> not_equal_to(PropertyType value_to_match);
    }
}