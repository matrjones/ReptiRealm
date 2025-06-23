/*TODO
* dates need to auto-populate with today's date and have the ability to change
* rating buttons default to green
* confirm entry message - confirm returns to MyReptiles, cancel goes back a step
*/

import 'package:flutter/material.dart';
import 'package:reptirealm/pages/shared/partials/header_bar.dart';
import 'package:reptirealm/pages/shared/partials/navigation_bar.dart';


class AddShed extends StatefulWidget {
  const AddShed({super.key});

  @override
  State<AddShed> createState() => _AddShedState();
}


class _AddShedState extends State<AddShed> {
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
                "Date in blue:",
                style: TextStyle(fontSize: 20, fontWeight: FontWeight.bold),
              ),
              Text(
                "Date shed:",
                style: TextStyle(fontSize: 20, fontWeight: FontWeight.bold),
              ),
              Text(
                "*Red button*",
                style: TextStyle(fontSize: 20, fontWeight: FontWeight.bold),
              ),
              Text(
                "*Orange button*",
                style: TextStyle(fontSize: 20, fontWeight: FontWeight.bold),
              ),
              Text(
                "*Green button*",
                style: TextStyle(fontSize: 20, fontWeight: FontWeight.bold),
              ),
              Text(
                "*Cancel / Save*",
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
