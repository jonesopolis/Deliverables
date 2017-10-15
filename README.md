# The Great Delivery Simulator

### Application

The application consists of a Console Application that can automate a delivery schedule, as well as a more user friendly web interface that will show the delivery system in real time.  The application was built to demonstrate my coding style, capitalizing on the benefits of base classes and reusable code.

### Code 
##### Domain (Deliverable Project)
- ITruck 
    - Red
    - Blue
    - Custom 
-   Delivery
##### Delivery Simulator
- Built using the template pattern
    - abstract base class defines process
    - Part of implementation deferred to derived classes
        - AutomatedDeliverySimulator runs simulation to completion
        - EventingDeliverySimulator runs at an hour per second, exposing a SimulationTickedEvent
        
##### Web
- Written as a single page app, refreshing the page will reset the simulator as there is no backing data store
- Add trucks and deliveries, pressing run will validate the simulation and start the process if valid, or return errors
- Server pushes updates to the client each hour (sped up to seconds)
- After the simulation is run, a report shows 
    - Truck total travel time
    - Deliveries for each truck
        - Departure time
        - Tracking number
        - Travel time 
        - Delivery time
- To Run
    - Ensure you have .NET Core 2.0 installed
    - building the application will run the bundler, building the site.min.css and site.min.js

##### Console
- the console app was developed simply to demonstrate the value of reusable code in the simulator

### Technology
The application was built on the latest version of .NET Core (2.0). It also employs SignalR for the real time communication system between the server and client to simulate deliveries.  Bootstrap was used to give the client a stylish interface without much effort, and JQuery is utilized to handle client side events.  All custom jquery files are bundled and minified to increase page speed response time.


