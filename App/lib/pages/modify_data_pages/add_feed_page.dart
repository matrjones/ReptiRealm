// /*TODO
// * date needs to auto-populate with today's date and have the ability to change
// * food type needs dropdown with auto-populate for specific default food set in settings - can manually input food type
// * number of food items defaulted to 1 (change with left/right buttons)
// * ability to add another food type with number eaten
// * Yes / No selection button
// * confirm entry message - confirm returns to MyReptiles, cancel goes back a step
// */


import 'package:flutter/material.dart';


class AddFeed extends StatefulWidget {
  const AddFeed({super.key});

  @override
  State<AddFeed> createState() => _AddFeedState();
}


class _AddFeedState extends State<AddFeed> {
  DateTime? selectedDate;
  List<FoodItem> foodItems = [FoodItem()];
  bool? wasFed;
  final TextEditingController notesController = TextEditingController();

  void _selectDate() async {
    final now = DateTime.now();
    final picked = await showDatePicker(
      context: context,
      initialDate: selectedDate ?? now,
      firstDate: DateTime(now.year - 5),
      lastDate: DateTime(now.year + 5),
    );
    if (picked != null) {
      setState(() {
        selectedDate = picked;
      });
    }
  }

  void _addFoodItem() {
    setState(() {
      foodItems.add(FoodItem());
    });
  }

  void _removeFoodItem(int index) {
    setState(() {
      foodItems.removeAt(index);
    });
  }

  void _cancel() {
    Navigator.pop(context);
  }

  void _save() {
    // Implement your save logic here
    print('Date: $selectedDate');
    for (var item in foodItems) {
      print('Food: ${item.selectedFood}, Qty: ${item.quantity}');
    }
    print('Fed: $wasFed');
    print('Notes: ${notesController.text}');
    Navigator.pop(context); // Or keep open and show confirmation
  }

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(title: const Text("Add Feed")),
      body: Padding(
        padding: const EdgeInsets.all(16.0),
        child: ListView(
          children: [

            // Date Picker
            ListTile(
              title: const Text("Date"),
              subtitle: Text(selectedDate != null
                  ? "${selectedDate!.toLocal()}".split(' ')[0]
                  : "Select a date"),
              trailing: IconButton(
                icon: const Icon(Icons.calendar_today),
                onPressed: _selectDate,
              ),
            ),

            const SizedBox(height: 16),

            // Food Items
            const Text("Food Items", style: TextStyle(fontWeight: FontWeight.bold)),
            ...foodItems.asMap().entries.map((entry) {
              final index = entry.key;
              final item = entry.value;
              return Row(
                children: [
                  Expanded(
                    flex: 2,
                    child: DropdownButtonFormField<String>(
                      value: item.selectedFood,
                      items: const [
                        DropdownMenuItem(value: 'Mouse', child: Text('Mouse')),
                        DropdownMenuItem(value: 'Rat', child: Text('Rat')),
                        DropdownMenuItem(value: 'Cricket', child: Text('Cricket')),
                        DropdownMenuItem(value: 'Dubia', child: Text('Dubia')),
                      ],
                      onChanged: (val) => setState(() => item.selectedFood = val),
                      decoration: const InputDecoration(labelText: "Food"),
                    ),
                  ),
                  const SizedBox(width: 10),
                  Expanded(
                    flex: 1,
                    child: TextFormField(
                      keyboardType: TextInputType.number,
                      decoration: const InputDecoration(labelText: "Qty"),
                      onChanged: (val) => item.quantity = int.tryParse(val) ?? 0,
                    ),
                  ),
                  IconButton(
                    icon: const Icon(Icons.delete),
                    onPressed: () => _removeFoodItem(index),
                  ),
                ],
              );
            }),
            TextButton.icon(
              onPressed: _addFoodItem,
              icon: const Icon(Icons.add),
              label: const Text("Add another food"),
            ),

            const SizedBox(height: 16),

            // Fed Yes/No
            const Text("Fed", style: TextStyle(fontWeight: FontWeight.bold)),
            Row(
              children: [
                Expanded(
                  child: RadioListTile<bool>(
                    title: const Text("Yes"),
                    value: true,
                    groupValue: wasFed,
                    onChanged: (val) => setState(() => wasFed = val),
                  ),
                ),
                Expanded(
                  child: RadioListTile<bool>(
                    title: const Text("No"),
                    value: false,
                    groupValue: wasFed,
                    onChanged: (val) => setState(() => wasFed = val),
                  ),
                ),
              ],
            ),

            const SizedBox(height: 16),

            // Notes
            TextFormField(
              controller: notesController,
              decoration: const InputDecoration(
                labelText: "Notes",
                border: OutlineInputBorder(),
              ),
              maxLines: 3,
            ),

            const SizedBox(height: 24),

            // Buttons
            Row(
              mainAxisAlignment: MainAxisAlignment.spaceEvenly,
              children: [
                OutlinedButton(
                  onPressed: _cancel,
                  child: const Text("Cancel"),
                ),
                ElevatedButton(
                  onPressed: _save,
                  child: const Text("Save"),
                ),
              ],
            )
          ],
        ),
      ),
    );
  }
}

// Helper class to track food item fields
class FoodItem {
  String? selectedFood;
  int quantity = 1;
}
