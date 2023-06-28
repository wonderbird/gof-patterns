using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Repository;

public sealed class Exercise : IEquatable<Exercise>
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }

    public DateTime Start { get; set; }

    public Exercise(DateTime start) => Start = start;

    public Exercise(ExerciseDto exerciseDto)
    {
        Id = exerciseDto.Id.ToString(CultureInfo.InvariantCulture);
        Start = exerciseDto.Start;
    }

    #region Equality Members

    public bool Equals(Exercise? other)
    {
        if (ReferenceEquals(null, other))
        {
            return false;
        }

        if (ReferenceEquals(this, other))
        {
            return true;
        }

        return Start.Equals(other.Start);
    }

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj))
        {
            return false;
        }

        if (ReferenceEquals(this, obj))
        {
            return true;
        }

        if (obj.GetType() != this.GetType())
        {
            return false;
        }

        return Equals((Exercise)obj);
    }

    [SuppressMessage(
        "Sonar Code Smell",
        "S3877:Exceptions should not be thrown from unexpected methods",
        Justification = "The Exercise entity is not a value object. Thus it should not be used with operations and data types relying on GetHashCode()."
    )]
    public override int GetHashCode() =>
        throw new NotSupportedException(
            "The Exercise entity is not a value object. Thus it should not be used with operations and data types relying on GetHashCode()."
        );

    public static bool operator ==(Exercise? left, Exercise? right) => Equals(left, right);

    public static bool operator !=(Exercise? left, Exercise? right) => !Equals(left, right);

    #endregion
}
