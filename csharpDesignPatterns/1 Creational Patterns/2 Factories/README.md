# Factories Design Pattern
`A component resposible solely for the wholesale (not piecewise) creation of objects.`

**Reasons**
1. Object creation logic becomes too convoluted
2. Constructor is not descriptive
	- Name mandated by name of containing type
	- Cannot overload with same sets of arguments with different names
	- Can turn into 'optional parameter hell'
3. Object creation (non-piecewise, unlike Builder) can be outsourced to
	- A separate function (Factory Method)
	- That may exist in a separate class (Factory)
	- Can create hierarchy of factories with Abstract Factory

**Summary**
- A factory method is static method that creates objects
- A factory can take cae of object creation
- A factory can be external or reside inside the object as an inner class
- Hierarchies of factories can be used to create related objects