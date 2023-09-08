# fin - a language for embedded systems

![image](https://github.com/fin-language/fin/assets/274012/226202a2-af98-4fe8-bfc5-718e6b719134)

Interesting things about fin:
- fin is like a modern C++ that is much simpler, way safer, and already has superb tooling (IDE, debugger, refactoring...).
- focuses on resource constrained embedded systems (no heap or garbage collector required).
- transpiles to high quality human readable C99 so we can use it with any microcontroller.
- has incredible testing/simulation capabilities (python like).
- familiar C/C++ syntax (be productive day 1).
- currently a work in progress.
- ...


<br>


# ☠️ Safety Stuff
- non-null references
- null analysis for pointers
- safer strings (also support c strings)
- safer arrays (also support c naked arrays)
- lightweight optional exceptions
- arithmetic overflow
- non-blocking assurances
- data thread safety
- selective safety escapes (for speed)

More info in [video 2](https://youtu.be/GMeskZM4wW0?si=9pD_PxQn6qty9vfT&t=63) or see pdf slides around slide 21.


<br>


# 📰 [pdf slides](https://github.com/fin-language/fin/files/12561990/slides.pdf)
If you prefer text over video, you can view the slides in pdf form. They lack some detail discussed in the video, but still have lots of info.

[slides.pdf](https://github.com/fin-language/fin/files/12561990/slides.pdf)


<br>


# [Video Series](https://youtube.com/playlist?list=PLe6ZYZk0KW45X2rLYI1IYco5774oe3Lmr)
I've just uploaded a youtube playlist that details fin, a bunch of stuff related to testing C embedded projects, multiple controller simulations, and the challenges that fin helps us solve.

![image](https://github.com/fin-language/fin/assets/274012/fe0af0c4-c2a9-4f9c-9ab3-8b02b88fc934)

## Video 1 - Write Embedded C Faster, Better, Safer With Fin
*   [0:00](https://www.youtube.com/watch?v=3KbB_0-K28U&list=PLe6ZYZk0KW45X2rLYI1IYco5774oe3Lmr&index=1&t=0s) no replacing C
*   [1:28](https://www.youtube.com/watch?v=3KbB_0-K28U&list=PLe6ZYZk0KW45X2rLYI1IYco5774oe3Lmr&index=1&t=88s) love C
*   [2:28](https://www.youtube.com/watch?v=3KbB_0-K28U&list=PLe6ZYZk0KW45X2rLYI1IYco5774oe3Lmr&index=1&t=148s) goals
*   [3:50](https://www.youtube.com/watch?v=3KbB_0-K28U&list=PLe6ZYZk0KW45X2rLYI1IYco5774oe3Lmr&index=1&t=230s) fin lang
*   [4:46](https://www.youtube.com/watch?v=3KbB_0-K28U&list=PLe6ZYZk0KW45X2rLYI1IYco5774oe3Lmr&index=1&t=286s) experimental
*   [6:04](https://www.youtube.com/watch?v=3KbB_0-K28U&list=PLe6ZYZk0KW45X2rLYI1IYco5774oe3Lmr&index=1&t=364s) readable C99
*   [7:48](https://www.youtube.com/watch?v=3KbB_0-K28U&list=PLe6ZYZk0KW45X2rLYI1IYco5774oe3Lmr&index=1&t=468s) subset of C#
*   [10:12](https://www.youtube.com/watch?v=3KbB_0-K28U&list=PLe6ZYZk0KW45X2rLYI1IYco5774oe3Lmr&index=1&t=612s) reasons for C#
*   [14:06](https://www.youtube.com/watch?v=3KbB_0-K28U&list=PLe6ZYZk0KW45X2rLYI1IYco5774oe3Lmr&index=1&t=846s) awesome testing!
*   [15:58](https://www.youtube.com/watch?v=3KbB_0-K28U&list=PLe6ZYZk0KW45X2rLYI1IYco5774oe3Lmr&index=1&t=958s) testing compared
*   [20:04](https://www.youtube.com/watch?v=3KbB_0-K28U&list=PLe6ZYZk0KW45X2rLYI1IYco5774oe3Lmr&index=1&t=1204s) python like
*   [21:42](https://www.youtube.com/watch?v=3KbB_0-K28U&list=PLe6ZYZk0KW45X2rLYI1IYco5774oe3Lmr&index=1&t=1302s) optional interfaces
*   [23:56](https://www.youtube.com/watch?v=3KbB_0-K28U&list=PLe6ZYZk0KW45X2rLYI1IYco5774oe3Lmr&index=1&t=1436s) generated c
*   [25:08](https://www.youtube.com/watch?v=3KbB_0-K28U&list=PLe6ZYZk0KW45X2rLYI1IYco5774oe3Lmr&index=1&t=1508s) choose 3
*   [25:36](https://www.youtube.com/watch?v=3KbB_0-K28U&list=PLe6ZYZk0KW45X2rLYI1IYco5774oe3Lmr&index=1&t=1536s) deploying fin

## Video 2 - Fin Language Features
*   [0:00](https://www.youtube.com/watch?v=GMeskZM4wW0&list=PLe6ZYZk0KW45X2rLYI1IYco5774oe3Lmr&index=2&t=0s) intro
*   [0:30](https://www.youtube.com/watch?v=GMeskZM4wW0&list=PLe6ZYZk0KW45X2rLYI1IYco5774oe3Lmr&index=2&t=30s) features covered
*   [1:30](https://www.youtube.com/watch?v=GMeskZM4wW0&list=PLe6ZYZk0KW45X2rLYI1IYco5774oe3Lmr&index=2&t=90s) no more headers!
*   [2:28](https://www.youtube.com/watch?v=GMeskZM4wW0&list=PLe6ZYZk0KW45X2rLYI1IYco5774oe3Lmr&index=2&t=148s) no null checks needed by default
*   [4:20](https://www.youtube.com/watch?v=GMeskZM4wW0&list=PLe6ZYZk0KW45X2rLYI1IYco5774oe3Lmr&index=2&t=260s) no pass by copy mistakes
*   [6:47](https://www.youtube.com/watch?v=GMeskZM4wW0&list=PLe6ZYZk0KW45X2rLYI1IYco5774oe3Lmr&index=2&t=407s) null analysis
*   [8:01](https://www.youtube.com/watch?v=GMeskZM4wW0&list=PLe6ZYZk0KW45X2rLYI1IYco5774oe3Lmr&index=2&t=481s) out parameters
*   [10:08](https://www.youtube.com/watch?v=GMeskZM4wW0&list=PLe6ZYZk0KW45X2rLYI1IYco5774oe3Lmr&index=2&t=608s) multiple returns with tuples
*   [11:38](https://www.youtube.com/watch?v=GMeskZM4wW0&list=PLe6ZYZk0KW45X2rLYI1IYco5774oe3Lmr&index=2&t=698s) named arguments
*   [12:48](https://www.youtube.com/watch?v=GMeskZM4wW0&list=PLe6ZYZk0KW45X2rLYI1IYco5774oe3Lmr&index=2&t=768s) simple generics
*   [16:50](https://www.youtube.com/watch?v=GMeskZM4wW0&list=PLe6ZYZk0KW45X2rLYI1IYco5774oe3Lmr&index=2&t=1010s) safer strings
*   [17:47](https://www.youtube.com/watch?v=GMeskZM4wW0&list=PLe6ZYZk0KW45X2rLYI1IYco5774oe3Lmr&index=2&t=1067s) safer arrays
*   [21:40](https://www.youtube.com/watch?v=GMeskZM4wW0&list=PLe6ZYZk0KW45X2rLYI1IYco5774oe3Lmr&index=2&t=1300s) overflow
*   [27:10](https://www.youtube.com/watch?v=GMeskZM4wW0&list=PLe6ZYZk0KW45X2rLYI1IYco5774oe3Lmr&index=2&t=1630s) non-blocking assurance
*   [29:58](https://www.youtube.com/watch?v=GMeskZM4wW0&list=PLe6ZYZk0KW45X2rLYI1IYco5774oe3Lmr&index=2&t=1798s) critical section audits
*   [31:02](https://www.youtube.com/watch?v=GMeskZM4wW0&list=PLe6ZYZk0KW45X2rLYI1IYco5774oe3Lmr&index=2&t=1862s) data thread safety
*   [32:14](https://www.youtube.com/watch?v=GMeskZM4wW0&list=PLe6ZYZk0KW45X2rLYI1IYco5774oe3Lmr&index=2&t=1934s) safety escapes
*   [33:30](https://www.youtube.com/watch?v=GMeskZM4wW0&list=PLe6ZYZk0KW45X2rLYI1IYco5774oe3Lmr&index=2&t=2010s) optional lightweight exceptions
*   [39:36](https://www.youtube.com/watch?v=GMeskZM4wW0&list=PLe6ZYZk0KW45X2rLYI1IYco5774oe3Lmr&index=2&t=2376s) support code gen
*   [41:30](https://www.youtube.com/watch?v=GMeskZM4wW0&list=PLe6ZYZk0KW45X2rLYI1IYco5774oe3Lmr&index=2&t=2490s) portable de/serialization
*   [42:53](https://www.youtube.com/watch?v=GMeskZM4wW0&list=PLe6ZYZk0KW45X2rLYI1IYco5774oe3Lmr&index=2&t=2573s) modular and efficient code

## Video 3 - Multiple Controller Simulations FTW
*   [0:00](https://www.youtube.com/watch?v=MCwpn5g342w&list=PLe6ZYZk0KW45X2rLYI1IYco5774oe3Lmr&index=3&t=0s) intro
*   [0:15](https://www.youtube.com/watch?v=MCwpn5g342w&list=PLe6ZYZk0KW45X2rLYI1IYco5774oe3Lmr&index=3&t=15s) no bok choy!
*   [0:50](https://www.youtube.com/watch?v=MCwpn5g342w&list=PLe6ZYZk0KW45X2rLYI1IYco5774oe3Lmr&index=3&t=50s) Java to C
*   [1:30](https://www.youtube.com/watch?v=MCwpn5g342w&list=PLe6ZYZk0KW45X2rLYI1IYco5774oe3Lmr&index=3&t=90s) my history
*   [4:18](https://www.youtube.com/watch?v=MCwpn5g342w&list=PLe6ZYZk0KW45X2rLYI1IYco5774oe3Lmr&index=3&t=258s) AVR love
*   [5:46](https://www.youtube.com/watch?v=MCwpn5g342w&list=PLe6ZYZk0KW45X2rLYI1IYco5774oe3Lmr&index=3&t=346s) my mistake
*   [7:02](https://www.youtube.com/watch?v=MCwpn5g342w&list=PLe6ZYZk0KW45X2rLYI1IYco5774oe3Lmr&index=3&t=422s) lessons learned
*   [8:57](https://www.youtube.com/watch?v=MCwpn5g342w&list=PLe6ZYZk0KW45X2rLYI1IYco5774oe3Lmr&index=3&t=537s) the product
*   [10:10](https://www.youtube.com/watch?v=MCwpn5g342w&list=PLe6ZYZk0KW45X2rLYI1IYco5774oe3Lmr&index=3&t=610s) test without pcb
*   [11:34](https://www.youtube.com/watch?v=MCwpn5g342w&list=PLe6ZYZk0KW45X2rLYI1IYco5774oe3Lmr&index=3&t=694s) fake com interfaces
*   [12:46](https://www.youtube.com/watch?v=MCwpn5g342w&list=PLe6ZYZk0KW45X2rLYI1IYco5774oe3Lmr&index=3&t=766s) sim radio phy
*   [16:38](https://www.youtube.com/watch?v=MCwpn5g342w&list=PLe6ZYZk0KW45X2rLYI1IYco5774oe3Lmr&index=3&t=998s) sim debugging
*   [18:48](https://www.youtube.com/watch?v=MCwpn5g342w&list=PLe6ZYZk0KW45X2rLYI1IYco5774oe3Lmr&index=3&t=1128s) sim fuzzing
*   [20:56](https://www.youtube.com/watch?v=MCwpn5g342w&list=PLe6ZYZk0KW45X2rLYI1IYco5774oe3Lmr&index=3&t=1256s) c code
*   [21:32](https://www.youtube.com/watch?v=MCwpn5g342w&list=PLe6ZYZk0KW45X2rLYI1IYco5774oe3Lmr&index=3&t=1292s) success
*   [22:22](https://www.youtube.com/watch?v=MCwpn5g342w&list=PLe6ZYZk0KW45X2rLYI1IYco5774oe3Lmr&index=3&t=1342s) why not java?

## Video 4 - Embedded C Software Testing & Design Choices
*   [0:00](https://www.youtube.com/watch?v=vE-LgMUnI9I&list=PLe6ZYZk0KW45X2rLYI1IYco5774oe3Lmr&index=4&t=0s) intro
*   [0:44](https://www.youtube.com/watch?v=vE-LgMUnI9I&list=PLe6ZYZk0KW45X2rLYI1IYco5774oe3Lmr&index=4&t=44s) how to test
*   [1:04](https://www.youtube.com/watch?v=vE-LgMUnI9I&list=PLe6ZYZk0KW45X2rLYI1IYco5774oe3Lmr&index=4&t=64s) dual target
*   [2:24](https://www.youtube.com/watch?v=vE-LgMUnI9I&list=PLe6ZYZk0KW45X2rLYI1IYco5774oe3Lmr&index=4&t=144s) test benefits
*   [3:48](https://www.youtube.com/watch?v=vE-LgMUnI9I&list=PLe6ZYZk0KW45X2rLYI1IYco5774oe3Lmr&index=4&t=228s) example product
*   [6:10](https://www.youtube.com/watch?v=vE-LgMUnI9I&list=PLe6ZYZk0KW45X2rLYI1IYco5774oe3Lmr&index=4&t=370s) example diagram
*   [6:50](https://www.youtube.com/watch?v=vE-LgMUnI9I&list=PLe6ZYZk0KW45X2rLYI1IYco5774oe3Lmr&index=4&t=410s) types of tests
*   [7:34](https://www.youtube.com/watch?v=vE-LgMUnI9I&list=PLe6ZYZk0KW45X2rLYI1IYco5774oe3Lmr&index=4&t=454s) unit tests
*   [9:32](https://www.youtube.com/watch?v=vE-LgMUnI9I&list=PLe6ZYZk0KW45X2rLYI1IYco5774oe3Lmr&index=4&t=572s) integration tests
*   [11:34](https://www.youtube.com/watch?v=vE-LgMUnI9I&list=PLe6ZYZk0KW45X2rLYI1IYco5774oe3Lmr&index=4&t=694s) faking/mocking approaches
*   [12:20](https://www.youtube.com/watch?v=vE-LgMUnI9I&list=PLe6ZYZk0KW45X2rLYI1IYco5774oe3Lmr&index=4&t=740s) using linker
*   [14:14](https://www.youtube.com/watch?v=vE-LgMUnI9I&list=PLe6ZYZk0KW45X2rLYI1IYco5774oe3Lmr&index=4&t=854s) using func ptrs
*   [16:08](https://www.youtube.com/watch?v=vE-LgMUnI9I&list=PLe6ZYZk0KW45X2rLYI1IYco5774oe3Lmr&index=4&t=968s) dual target challenges
*   [18:38](https://www.youtube.com/watch?v=vE-LgMUnI9I&list=PLe6ZYZk0KW45X2rLYI1IYco5774oe3Lmr&index=4&t=1118s) test accuracy
*   [20:52](https://www.youtube.com/watch?v=vE-LgMUnI9I&list=PLe6ZYZk0KW45X2rLYI1IYco5774oe3Lmr&index=4&t=1252s) testing C chores
*   [22:04](https://www.youtube.com/watch?v=vE-LgMUnI9I&list=PLe6ZYZk0KW45X2rLYI1IYco5774oe3Lmr&index=4&t=1324s) worth the pain
*   [23:44](https://www.youtube.com/watch?v=vE-LgMUnI9I&list=PLe6ZYZk0KW45X2rLYI1IYco5774oe3Lmr&index=4&t=1424s) test code matters
*   [27:42](https://www.youtube.com/watch?v=vE-LgMUnI9I&list=PLe6ZYZk0KW45X2rLYI1IYco5774oe3Lmr&index=4&t=1662s) fin stuff
*   [29:10](https://www.youtube.com/watch?v=vE-LgMUnI9I&list=PLe6ZYZk0KW45X2rLYI1IYco5774oe3Lmr&index=4&t=1750s) single or multi?
*   [32:06](https://www.youtube.com/watch?v=vE-LgMUnI9I&list=PLe6ZYZk0KW45X2rLYI1IYco5774oe3Lmr&index=4&t=1926s) flexible code costs
*   [34:54](https://www.youtube.com/watch?v=vE-LgMUnI9I&list=PLe6ZYZk0KW45X2rLYI1IYco5774oe3Lmr&index=4&t=2094s) RAM free vtable

## Video 5 - Language Comparison (for embedded)
*   [0:00](https://www.youtube.com/watch?v=bpIKURL2De0&list=PLe6ZYZk0KW45X2rLYI1IYco5774oe3Lmr&index=5&t=0s) intro
*   [0:56](https://www.youtube.com/watch?v=bpIKURL2De0&list=PLe6ZYZk0KW45X2rLYI1IYco5774oe3Lmr&index=5&t=56s) why not C++?
*   [2:06](https://www.youtube.com/watch?v=bpIKURL2De0&list=PLe6ZYZk0KW45X2rLYI1IYco5774oe3Lmr&index=5&t=126s) poor C++ tooling
*   [3:32](https://www.youtube.com/watch?v=bpIKURL2De0&list=PLe6ZYZk0KW45X2rLYI1IYco5774oe3Lmr&index=5&t=212s) C++ multi project refactoring
*   [7:18](https://www.youtube.com/watch?v=bpIKURL2De0&list=PLe6ZYZk0KW45X2rLYI1IYco5774oe3Lmr&index=5&t=438s) best C++ IDE?
*   [8:08](https://www.youtube.com/watch?v=bpIKURL2De0&list=PLe6ZYZk0KW45X2rLYI1IYco5774oe3Lmr&index=5&t=488s) refactoring for wimps?
*   [9:06](https://www.youtube.com/watch?v=bpIKURL2De0&list=PLe6ZYZk0KW45X2rLYI1IYco5774oe3Lmr&index=5&t=546s) other langs?
*   [10:44](https://www.youtube.com/watch?v=bpIKURL2De0&list=PLe6ZYZk0KW45X2rLYI1IYco5774oe3Lmr&index=5&t=644s) compile to C benefits

## Video 6 - Fin Implementation & Challenges
*   [0:00](https://www.youtube.com/watch?v=ITn2S3NVuM0&list=PLe6ZYZk0KW45X2rLYI1IYco5774oe3Lmr&index=6&t=0s) intro
*   [0:26](https://www.youtube.com/watch?v=ITn2S3NVuM0&list=PLe6ZYZk0KW45X2rLYI1IYco5774oe3Lmr&index=6&t=26s) lose xyz?
*   [0:50](https://www.youtube.com/watch?v=ITn2S3NVuM0&list=PLe6ZYZk0KW45X2rLYI1IYco5774oe3Lmr&index=6&t=50s) how is this possible?
*   [2:16](https://www.youtube.com/watch?v=ITn2S3NVuM0&list=PLe6ZYZk0KW45X2rLYI1IYco5774oe3Lmr&index=6&t=136s) no heap?
*   [3:02](https://www.youtube.com/watch?v=ITn2S3NVuM0&list=PLe6ZYZk0KW45X2rLYI1IYco5774oe3Lmr&index=6&t=182s) using syntax
*   [3:42](https://www.youtube.com/watch?v=ITn2S3NVuM0&list=PLe6ZYZk0KW45X2rLYI1IYco5774oe3Lmr&index=6&t=222s) const?
*   [5:32](https://www.youtube.com/watch?v=ITn2S3NVuM0&list=PLe6ZYZk0KW45X2rLYI1IYco5774oe3Lmr&index=6&t=332s) volatile?
*   [7:54](https://www.youtube.com/watch?v=ITn2S3NVuM0&list=PLe6ZYZk0KW45X2rLYI1IYco5774oe3Lmr&index=6&t=474s) C# downsides
*   [8:34](https://www.youtube.com/watch?v=ITn2S3NVuM0&list=PLe6ZYZk0KW45X2rLYI1IYco5774oe3Lmr&index=6&t=514s) reduce verbosity
*   [9:54](https://www.youtube.com/watch?v=ITn2S3NVuM0&list=PLe6ZYZk0KW45X2rLYI1IYco5774oe3Lmr&index=6&t=594s) custom code fixes
*   [11:26](https://www.youtube.com/watch?v=ITn2S3NVuM0&list=PLe6ZYZk0KW45X2rLYI1IYco5774oe3Lmr&index=6&t=686s) our code fixes
*   [12:04](https://www.youtube.com/watch?v=ITn2S3NVuM0&list=PLe6ZYZk0KW45X2rLYI1IYco5774oe3Lmr&index=6&t=724s) fin costs
*   [14:44](https://www.youtube.com/watch?v=ITn2S3NVuM0&list=PLe6ZYZk0KW45X2rLYI1IYco5774oe3Lmr&index=6&t=884s) rough plan
*   [16:46](https://www.youtube.com/watch?v=ITn2S3NVuM0&list=PLe6ZYZk0KW45X2rLYI1IYco5774oe3Lmr&index=6&t=1006s) can I do this?
*   [17:38](https://www.youtube.com/watch?v=ITn2S3NVuM0&list=PLe6ZYZk0KW45X2rLYI1IYco5774oe3Lmr&index=6&t=1058s) you can help


<br>


# Feedback
The fin compiler isn't ready to share yet, but I do already transpile to C and js in [StateSmith](https://github.com/StateSmith/StateSmith). I'm not too worried about the transpiling work.

What I'm looking for mostly right now is some [feedback on the concept](https://github.com/fin-language/fin/issues/2).

In the next couple weeks, I'll be adding proposals for syntax to use for stack allocating objects and other stuff. More videos too.

For the most part, I just wanted to get it out there and stop working by myself in a vacuum. Especially if there is a language I'm not aware of that already does what I'm planning.


<br>


# Some intro slides
YouTube playlist above has best info (pdf is pretty good too).

![image](https://github.com/fin-language/fin/assets/274012/226202a2-af98-4fe8-bfc5-718e6b719134)

![image](https://github.com/fin-language/fin/assets/274012/9e451fcd-7e4b-475d-a2e9-444279bbf32b)

![image](https://github.com/fin-language/fin/assets/274012/1a5fe6f9-2a3b-42e2-b9ea-2b769fb16deb)

![image](https://github.com/fin-language/fin/assets/274012/7db5e0dd-86f1-4623-aeff-537d8d9b8157)

![image](https://github.com/fin-language/fin/assets/274012/ca0ffbd9-26a2-4acf-885a-5c1aa3fb7273)

![image](https://github.com/fin-language/fin/assets/274012/e08fdd16-0d3b-48ee-9ec9-ad87b05b0664)

![image](https://github.com/fin-language/fin/assets/274012/63d133c0-85c7-42e0-866a-8a66811beae5)

![image](https://github.com/fin-language/fin/assets/274012/a2dcb49e-a0b2-4dd3-9a7a-6116b7e04753)

![image](https://github.com/fin-language/fin/assets/274012/e5a980b1-bc84-415b-81f6-f5ab97955e2b)

![image](https://github.com/fin-language/fin/assets/274012/62646934-18e6-4a47-82d9-76dbe3a63071)

![image](https://github.com/fin-language/fin/assets/274012/f9381ece-1908-4176-b677-446222f9e2d4)

![image](https://github.com/fin-language/fin/assets/274012/87866ea9-1542-4976-923e-8d2f1db684f4)

![image](https://github.com/fin-language/fin/assets/274012/c3faf9be-2fa2-441d-b070-b1088ac1d501)

![image](https://github.com/fin-language/fin/assets/274012/c1ff9c58-cf33-4153-85b4-7b854f51148d)

![image](https://github.com/fin-language/fin/assets/274012/2e700366-7d04-41fe-9f71-12785c12fb85)

![image](https://github.com/fin-language/fin/assets/274012/8b66297d-265d-4207-9716-8b78443764a6)

![image](https://github.com/fin-language/fin/assets/274012/6cf13c1c-98e6-4b06-a3a2-23cfc4041b50)

![image](https://github.com/fin-language/fin/assets/274012/947b758a-47fd-4436-9a57-b6ebd74092d2)

![image](https://github.com/fin-language/fin/assets/274012/a8ad8717-85a1-43c6-bd01-30227aa34528)

## More info
- https://github.com/fin-language/fin/blob/main/README.md#-pdf-slides
- https://youtube.com/playlist?list=PLe6ZYZk0KW45X2rLYI1IYco5774oe3Lmr


