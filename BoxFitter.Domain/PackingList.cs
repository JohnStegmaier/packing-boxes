namespace BoxFitter.Domain;

public record PackingList(
    List<Box> PackedBoxes,
    List<Book> BooksThatCannotBePacked
        );