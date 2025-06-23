/*TODO
* date needs to auto-populate with today's date and have the ability to change
* type needs a dropdown
* comment box needs implementing
* confirm entry message - confirm returns to MyReptiles, cancel goes back a step
*/

import 'package:flutter/material.dart';
import 'package:reptirealm/pages/shared/partials/header_bar.dart';
import 'package:reptirealm/pages/shared/partials/navigation_bar.dart';


class AddDefecation extends StatefulWidget {
  const AddDefecation({super.key});

  @override
  State<AddDefecation> createState() => _AddDefecationState();
}


class _AddDefecationState extends State<AddDefecation> {
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
                "Type:",
                style: TextStyle(fontSize: 20, fontWeight: FontWeight.bold),
              ),
              Text(
                  "Comment Box:",
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
