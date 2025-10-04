--IMAGE 
insert into image values (null, 'https://static.vecteezy.com/system/resources/thumbnails/009/292/244/small_2x/default-avatar-icon-of-social-media-user-vector.jpg');
insert into image values (null, 'https://www.reddit.com/media?url=https%3A%2F%2Fi.redd.it%2F457zso4lmsjb1.jpg');
insert into image values (null, 'https://diycandy.b-cdn.net/wp-content/uploads/2021/06/Pile-of-rainbow-yarn-in-various-colors.jpeg');
insert into image values (null, 'lazni_link_AHA.jpeg');
insert into image values (null, 'https://sarahmaker.com/wp-content/uploads/2021/01/crochet-heart-pattern-9.jpg');


--APPUSER
insert into appuser values (null,'Advanced', 'user2@gmail.com', 'pass', 'user2',1, 'Creator');
insert into appuser values (null,'Advanced', 'user1@gmail.com', 'pass', 'user1',1, 'Admin');
insert into appuser values (null,'Beginner', 'user3@gmail.com', 'pass', 'user3',1, 'Regular');
insert into appuser values (null,'Beginner', 'userzaupdate@gmail.com', 'pass', 'TBA',2, 'Regular');
insert into appuser values (null,'Beginner', 'userzabrisanje@gmail.com', 'pass', 'REMOVE',2, 'Regular');

--YARN
insert into yarn values (null,'chennile extra soft', 'worseted', 'chenile', 100, 4.5, 8, 'pink');
insert into yarn values (null,'chennile extra soft', 'worseted', 'chenile', 100, 4.5, 8, 'red');
insert into yarn values (null,'chennile extra soft', 'worseted', 'chenile', 100, 4.5, 8, 'yellow');
insert into yarn values (null,'chennile extra soft', 'worseted', 'chenile', 100, 4.5, 8, 'green');
insert into yarn values (null,'chennile extra soft', 'worseted', 'chenile', 100, 4.5, 8, 'white');
insert into yarn values (null,'alize velutto', 'fluffy', 'plush', 100, 8, 10, 'baby blue');
insert into yarn values (null,'alize puffy finger loop', 'finger loop', 'micropolyester', 100, 0, 0, 'pink');

--HOOK
insert into hook values (null,6);
insert into hook values (null,6.5);
insert into hook values (null,5.5);
insert into hook values (null,3);
insert into hook values (null,4);
insert into hook values (null,1);
insert into hook values (null,1.3);
insert into hook values (null,4.5);
insert into hook values (null,3.5);
insert into hook values (null,8);

--TECHNIQUE
insert into technique values (null,'single crochet', 'Beginner');
insert into technique values (null,'double crochet', 'Beginner');
insert into technique values (null,'treble crochet', 'Beginner');
insert into technique values (null,'popcorn stitch', 'Advanced');
insert into technique values (null,'puff stitch', 'Advanced');
insert into technique values (null,'invisible join', 'Advanced');
insert into technique values (null,'increase', 'Beginner');
insert into technique values (null,'decrease', 'Beginner');
insert into technique values (null,'chain', 'Beginner');

--CATEGORY
insert into category values (null, 'amigurumi toys');
insert into category values (null, 'granny square');
insert into category values (null, 'wearable');
insert into category values (null, 'blanket');
insert into category values (null, 'decoration');
insert into category values (null, 'trinkets');
insert into category values (null, 'accessories');
insert into category values (null, 'tapestry');

--TAG
insert into tag values (null, 'fluffy');
insert into tag values (null, 'child friendly');
insert into tag values (null, 'halloween');
insert into tag values (null, 'spring');
insert into tag values (null, 'winter');
insert into tag values (null, 'valentines');
insert into tag values (null, 'fandoms');
insert into tag values (null, 'flowers');


--PATTERN
insert into pattern values (null, 'PLACEHOLDER PATTERN', 'PLACEHOLDER DESC', 'Beginner', '01-Nov-2024', 3.4, 'STEPS', 'Rejected');
insert into pattern values (null, 'Easy and Quick Crochet Heart Pattern',
                                    'This is a super simple, easy to make crochet heart pattern. Perfect for any time you want to tell
                                    someone you love them! I also love the idea of leaving them in the street with little love notes for
                                    strangers. Any way to spread the love! You can pop these on presents, cards or simply hang them
                                    anywhere you can find a spare spot. ',
                                    'Beginner', '01-Nov-2024', 3.4, 
                                    'Row 1 - Sc in 2nd chain from hook, sc in each ch across (10 st) 
                                    Row 2 - Ch 1, turn. Sc in each st across. (10 st)
                                    (...)
                                    Weave in ends.
                                    Gently pat with water and pin into place on a blocking mat and wait until dry.',
                                    'Approved');
insert into pattern values (null, 'UPDATE YOUR PATTERN', 'UPDATE YOUR DESC', 'Beginner', '01-Nov-2024', 3.4, 'STEPS', 'Pending');

--REQUEST
insert into request values (null, '01-Nov-2024', 'Approved', 3, 2, 2);
insert into request values (null, '01-Nov-2020', 'Rejected', null, 1, 1);
insert into request values (null, '01-Nov-2020', 'Rejected', null, 1, 3);

--TUTORIAL
insert into tutorial values (null, 'PLACEHOLDER TEXT','videolink', 'Beginner', 'PLACEHOLDER TUTORIAL', 1);
insert into tutorial values (null, 'UPDATE','videolink', 'Beginner', 'UPDATE', 1);
insert into tutorial values (null, 'DELETE','videolink', 'Beginner', 'DELETE', 1);

--LIBRARY
insert into library values (null, 'PLACEHOLDER LIB', 'PLACEHOLDER DESC', '01-nov-2021', 3); 
insert into library values (null, 'FUN LIBRARY UWU', 'YAYAYAYAYAYAYAY', '01-nov-2021', 3); 
insert into library values (null, 'BADLIB DELETE', 'DELETE', '01-nov-2021', 3); 

--SUGGESTION
insert into suggestion values (null, 'PLACE HOLDER TEXT', 3);
insert into suggestion values (null, 'pls make fluffy bunny', 4);
insert into suggestion values (null, 'anyone have textile patterns to share?', 5);

--PROJECT
insert into project values (null, 0.0, 'Ongoing', '01-nov-2020', '01-nov-2020', 'NOTES', null, 'PLACEHOLDER PROJECT');
insert into project values (null, 0.0, 'Ongoing', '01-nov-2020', '01-nov-2020', 'NOTES', 1, 'LITTLE PLACEHOLDER PROJECT');
INSERT INTO PROJECT VALUES (null, 0, 'Ongoing', '21-aug-2021', '21-aug-2021', 'notes', 1, '2ND LITTLE PROJECT');
INSERT INTO PROJECT VALUES (null, 100, 'Finished', '21-aug-2024', '10-may-2025', 'notes', null, '2ND PARENT');
INSERT INTO PROJECT VALUES (null, 100, 'Finished', '21-aug-2024', '14-sep-2024', 'notes', 4, '2NDS LITTLE');
INSERT INTO PROJECT VALUES (null, 100, 'Finished', '28-jan-2025', '10-may-2025', 'notes', 4, '2NDS 2ND LITTLE');



--USER BEGINS PROJECT
insert into begins values (1,1);
insert into begins values (3,4);
insert into begins values (3,5);


--PROJECT UTILIZES PATTERN
insert into utilizes values (1,1);
insert into utilizes values (4,2);
insert into utilizes values (4,3);
insert into utilizes values (5,1);

--USER REVIEWS THE PATTERN UTILIZED BY THE PROJECT
insert into reviews VALUES (1,1,1,1, 'PLACEHOLDER', '01-nov-2020', 3);

--USER'S LIBRARY CONTAINS PATTERNS
insert into consistsof values (1,1,3);
insert into consistsof values (1,2,3);
insert into consistsof values (2,2,3);
insert into consistsof values (3,2,3);
insert into consistsof values (1,3,3);

--YARN IS USED WITH THIS HOOK
insert into designedfor values (1,1);

--TUTORIAL EXPLAINS THIS TECHNIQUE 
-- tech, tuto, user
insert into explains values (1,1,1);
insert into explains values (5,1,1);
insert into explains values (6,1,1);
insert into explains values (2,2,1);
insert into explains values (3,2,1);
insert into explains values (4,2,1);
insert into explains values (7,3,1);
insert into explains values (8,3,1);
insert into explains values (9,3,1);
insert into explains values (1,3,1);

--PATTERN FALLS UNDER THIS CATEGORY
insert into fallsunder values (1,1);
insert into fallsunder values (5,2);
insert into fallsunder values (6,2);
insert into fallsunder values (7,2);

--CREATOR FULLFILLS REQUEST
insert into fulfills values (2,1);

--PATTERN HAS IMAGES
insert into has values (1,1);
insert into has values (2,1);
insert into has values (3,1);
insert into has values (5,2);

--PATTERN IMPLEMENTS TECHNIQUE
insert into implements values (1, 1);
insert into implements values (1, 2);
insert into implements values (3, 2);
insert into implements values (9, 2);

--IMAGE IS OPTIONAL FOR THE TUTORIAL
insert into isoptional values (1,1,1);

--PATTERN IS TAGGED
insert into istagged values (1, 1);

--PATTERN RECOMMENDS HOOK
insert into recommends values (1,1);

--IMAGE REPRESENTS SUGGESTION
insert into represents values (1,1);

--PATTERN USES YARN
insert into uses values (1,1);

--TUTORIAL USES YARN
insert into workswith values (1,1,1);

--USER KNOWS TECHNIQUE
insert into knows values (1,1);
insert into knows values (2,1);
insert into knows values (3,1);

--TUTORIAL OPERATES WITH HOOK
insert into OperatesWith values (1, 1, 1);

--Tutorial helps a pattern
insert into helps values(1,1,1);

commit;




--TESTING STATEMENTS;

-- query that fetches the patterns used within their projects,
-- aka only patterns theyre allowed to rate
with ToReview as (
select patternId as retId
from utilizes 
inner join begins 
on utilizes.projectid = begins.projectid 
where begins.appuserId = 3) 
select * 
from pattern 
inner join ToReview 
on patternId = retId
order by patternId;
