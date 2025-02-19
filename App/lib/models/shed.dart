class Shed {
  final String? id;
  final DateTime date;
  final String blueOrShed;
  final String rating;
  final String? comment;

  Shed({
    this.id,
    required this.date,
    required this.blueOrShed,
    required this.rating,
    this.comment,
  });
}
