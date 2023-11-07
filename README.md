# DiningPhilosophers, Pararell problem with OOP patterns
https://en.wikipedia.org/wiki/Dining_philosophers_problem
In my Djiskra implemantation i used SlimSemaphore[]. It handless up to 34 philosophers with 1 to 2 ms intervals for using resources. When adding more than 34 philosophers the others remain starving.

My Chandy/Misra implemantation is using Monitor and a while loop. It should be solution which distributes resources at lower rate, but can have more consumers. In my case the rate seems fine, but it can only handle 6 consumers and then gets deadlocked.

I also implemented OOP design patterns. After the refactor Chandy/Misra solution became less efficient, further prooving that there is something wrong in my implemantation.
