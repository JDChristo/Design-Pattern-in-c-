# Builder Design Pattern

**Reasons**
1. Some objects are simple and can be created in a single constructer call
2. Other objects require a lot of ceremony to create
3. Having an object with 10 constructor arguments is not productive
4. Instead, option for piecewise construction
5. Builder provides an API for constructing an object step-by-step

`When piecewise object construction is complicated, provide an API for doing it succinctly`