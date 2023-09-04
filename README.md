# Shoot-the-Shapes
Create a Mini Shooting Game "Shoot the Shapes".

Technical Specification:
You have two 3D objects at your disposal: a cube and a sphere. Using mathematical formulas, you need to create the following shapes: circle, rectangle, pyramid, and square. The shapes should appear randomly at different distances from the player.

Each of the objects (cube and sphere) should have a random color. Colors are taken from a configuration file read from JSON (colors of your choice).

The ball used to shoot at the shapes should also change color with each shot. The ball's color is also taken from the JSON configuration file (colors of your choice).

When the ball hits a shape, the shape should be destroyed with physics simulation (i.e., it should shatter). After the destruction of a shape, a new one is created, and the camera should smoothly rotate towards it. After the rotation, the player should be ready for the next shot.

Player Control:
The player cannot move. They can only rotate around their axis and change the direction of their view up and down. This functionality is achieved through a joystick.

Shooting by the player is also done using the joystick.
You can use any asset for the joystick.
