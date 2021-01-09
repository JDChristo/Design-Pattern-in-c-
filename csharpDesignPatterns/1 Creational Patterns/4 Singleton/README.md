# Prototype Design Pattern
`A component which is instatiated only once.`

**Motivation**
1. For some components it only makes sense to have one in the system
	- Database respository
	- Object factory
2. E.g., the constructor call is expensive
	- We only do it once
	- We provide everyone with the same instance
3. Want to prevent anyone creating additional copies
4. Need to take care of lazy instantiation and thread safety

**Summary**
- Making a safe singleton is easy:: contruct a `static Lazy<T>` and return its value
- Singletons are difficult to ttest
- Insteadada of directly using a singleton, consider depending on an abstraction (e.g., an interfaace)
- Consider defining singleton lifetime in DI container
