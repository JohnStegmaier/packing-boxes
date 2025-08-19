![Cover Image](./Repofiles/Images/Boxfitter%20Image%20Logo.png)
# Packing Boxes
This is an in-development application that helps pack educational books into boxes for shipping.

## Problem Statement
Currently, there exists a warehouse that faces a problem whereby their current warehouse management software fails to correctly and efficiently generate a packing list resulting in excessive shipping costs. This issue comes from a bug in the current software whereby it fails to handle packing books of half width side-by-side in a box.

### Simplified Example
Let's say you have a box that is 30 cm in width, length and height and a book that is 30 cm in length, 15 cm in width and 5 cm in height. If an order was received to pack 12 books, the current system would generate a packing list for two boxes; ignoring the possibility of stacking books side-by-side in the same box:
![Example Image](./Repofiles/Images/Example%20Image.png)
The desired solution for this would be to instead pack the books side-by-side into a single box since the books should be able to fit beside each-other width wise; the box is 30 cm wide, and the books are each 15 cm wide, allowing two to fit perfectly side-by-side.

### Additional considerations
The simplified example above defines a shallow scope for the problem. In reality, we have boxes of three heights _(40 cm, 60 cm and 80 cm)_. Their Length and Width are fixed parameters, and the books will always either fit the length and width perfectly or fit the length perfectly and **exactly** half the size of the box (so that two books can be packed side-by-side).

#### Example Data
A small mocked example of the book data was provided in [`sampleDatabaseMetric.csv`](./BoxFitter/sampleDatabaseMetric.csv). This is assumptively how the application would be receiving the data on the books:

| Item - Number | Item - Name | Height | FW \| HW |
| --- | --- |-------:| --- |
| 3 | Spanish 3 Big Book |   3.05 | FW |
| 6 | Linear Algebra |   6.73 | FW |
| 8 | Data Structures |   6.86 | FW |
| 11 | DVORAK Typing for Dummies |   7.37 | FW |
| 14 | Book of 3.3 Inches |   8.38 | FW |
| 16 | How to use Git | 11.43  | FW |
| 17 | Calculus |     11.94 | FW |
| 1 | Spanish 1 |   1.27 | HW |
| 2 | Spanish 2 |   2.29 | HW |
| 4 | Calculus Answer Key |   3.81 | HW |
| 5 | Everything to Know About Rats & Mice |   4.83 | HW |
| 7 | What OSHA rules really ought to be updated |   6.86 | HW |
| 9 | Agile Manifesto |   6.86 | HW |
| 10 | The 500 ways to use the words "Modus Operandi" in normal conversation |      6.99 | HW |
| 12 | How to Kill Several Mockingbirds |   7.62 | HW |
| 13 | Why test driven development is wrong |      8.13 | HW |
| 15 | Why test driven development is right |    8.64 | HW |

The first column is the item SKU (unique identifier) and should be handled as a string. The next Column is the name of the book followed by its height in centimeters and then finally whether the book is a full width or half width.