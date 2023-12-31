﻿using ErrorOr;

namespace LuckyFoodSystem.Domain.AggregationModels.Errors
{
    public static partial class Errors
    {
        public static class Global
        {
            public static Error CollectionNonExistentException
                => Error.NotFound(code: "NullableCollection",
                                  description: "Selected collection is non-existent");
            public static Error ObjectNonExistentException
                => Error.NotFound(code: "NonExistentObject",
                                  description: "Selected object is non-existent");
            public static Error ObjectNotRemovedException
                => Error.NotFound(code: "NotRemovedObject",
                                  description: "Obj with this id is non-existent or cannot be deleted");
        }
    }
}
