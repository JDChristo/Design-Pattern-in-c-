# Bridge Design Pattern
`A mechnism that decouples an interface (hierarchy) from an implementation (hierarchy)s.`

**Motivation**
- Bridge prevents a 'Cartesian product' complexity explosion;
- Example:
        - Base class ThreadScheduler
        - Can be prrmptive or cooperative
        - Can run on Windows or Unix
- Brige pattern avoids the entity explosion

**Example**
Before
```csharp
/*                                  [==============================]
*                                   [                              ]
*                                   [       ThreadScheduler        ]
*                                   [                              ]
*                                   [==============================]
*                                                   ^
*                                  _________________|__________________
*                                  |                                  |
*                                  V                                  V
*                  ===============================]        [==============================]      
*                  [                              ]        [                              ]      
*                  [  PreemptiveThreadScheduler   ]        [ CooperativeThreadScheduler   ]      
*                  [                              ]        [                              ]      
*                  [==============================]        [==============================]
*                                        ^                               ^
*                  ______________________|                               |__________________________
*                  |                     |                               |                         |
*                  V                     |                               |                         V
*   [==============================]     |                               |          [==============================]
*   [                              ]     |                               |          [                              ]
*   [       WindowsPTS             ]     |                               |          [       WindowsPTS             ]
*   [                              ]     |                               |          [                              ]
*   [==============================]     |                               |          [==============================]
*                                        |                               |
*                                        |                               |
*                                        |                               |
*                         [==============================]    [==============================]
*                         [                              ]    [                              ]
*                         [          UnixPTS             ]    [          UnixCTS             ]
*                         [                              ]    [                              ]
*                         [==============================]    [==============================]
*
*
*
*
*
*/
```
After
```csharp
/*                                  [==============================]                             
*                                   [                              ]                             [==============================]
*                                   [       ThreadScheduler        ]                             [                              ]
*                                   [------------------------------]---------------------------->[      IPlatformScheduler      ]
*                                   [       -pltformcheduler       ]                             [                              ]
*                                   [==============================]                             [==============================]
*                                                   ^                                                               ^                                                       
*              _____________________________________|                          _____________________________________|                                                       
*              |                                                               |                                                       
*              |   [==============================]                            |   [==============================]                                                                    
*              |   [                              ]                            |   [                              ]                                                                    
*              |---[  PreemptiveThreadScheduler   ]                            |---[      UnixScheduler           ]                                                                    
*              |   [                              ]                            |   [                              ]                                                                    
*              |   [==============================]                            |   [==============================]                                                                    
*              |                                                               |                                                        
*              |   [==============================]                            |   [==============================]                                                        
*              |   [                              ]                            |   [                              ]                                                                    
*              |---[ CooperativeThreadScheduler   ]                            |---[      WindowScheduler         ]                                                        
*                  [                              ]                                [                              ]                                                        
*                  [==============================]                                [==============================]                                                        
*
*
*/
```
 

**Summary**
- Decouplpe abstraction from implementation
- Both can exist as hierarchies
- A stronger form of encapsulation
