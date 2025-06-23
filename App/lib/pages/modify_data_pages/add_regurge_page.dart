/*TODO
* date needs to auto-populate with today's date and have the ability to change
* food type needs dropdown with auto-populate for specific default food set in settings - can manually input food type
* number of food items defaulted to 1 (change with left/right buttons)
* ability to add another food type with number eaten
* Yes / No selection button
* confirm entry message - confirm returns to MyReptiles, cancel goes back a step
*/

import 'package:flutter/material.dart';
import 'package:reptirealm/pages/shared/partials/header_bar.dart';
import 'package:reptirealm/pages/shared/partials/navigation_bar.dart';


class AddRegurge extends StatefulWidget {
  const AddRegurge({super.key});

  @override
  State<AddRegurge> createState() => _AddRegurgeState();
}


class _AddRegurgeState extends State<AddRegurge> {
  @override
  Widget build(BuildContext context) {
    return const Scaffold(

      appBar: HeaderBar(),

      body: Align(
        alignment: Alignment.topLeft, // Moves column to the right side
        child: Padding(
          padding: EdgeInsets.all(16.0), // Adds spacing from edges
          child: Column(
            mainAxisSize: MainAxisSize.min,
            // Keeps it compact
            crossAxisAlignment: CrossAxisAlignment.start,
            // Aligns text to the LEFT
            children: [
              Text(
                "Date:",
                style: TextStyle(fontSize: 20, fontWeight: FontWeight.bold),
              ),
              Text(
                "Select feed:",
                style: TextStyle(fontSize: 20, fontWeight: FontWeight.bold),
              ),
              Text(
                "Comment box:",
                style: TextStyle(fontSize: 20, fontWeight: FontWeight.bold),
              ),
            ],
          ),
        ),
      ),

      bottomNavigationBar: NavBar(),
    );
  }
}
