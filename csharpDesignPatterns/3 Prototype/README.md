# Prototype Design Pattern
`A partially or fully initialized object that you clone and use the clone`
`When it's easier to copy an existing object to fully initialize a new one`

**Motivation**
1. Complicated objects (e.g., cars) aren't designed from scratch
	- They reiterate existing designs
2. An existing (paratially or fully constructed) design is a Prototype
3. We make a copy (clone) the prototype and customize it
	- Requires 'deep copy' support
4. We make the cloning convenient( e.g., via a Factory)

**Summary**
- To implement a prototype. partially construct an object and store it somewhere
- Clone the prototype
	- Implement your own deep copy functionality
	- Serialize and deserialize
- Customize the resulting instance