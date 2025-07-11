package model

import (
    "time"
    "fmt"
    "strings"
)

type DateOnly struct {
    time.Time
}

const dateLayout = "2006-01-02"

func (d *DateOnly) UnmarshalJSON(b []byte) error {
    s := strings.Trim(string(b), `"`)
    if s == "null" || s == "" {
        d.Time = time.Time{}
        return nil
    }
    t, err := time.Parse(dateLayout, s)
    if err != nil {
        return fmt.Errorf("invalid date: %w", err)
    }
    d.Time = t
    return nil
}

func (d DateOnly) MarshalJSON() ([]byte, error) {
    if d.Time.IsZero() {
        return []byte(`null`), nil
    }
    return []byte(fmt.Sprintf(`"%s"`, d.Time.Format(dateLayout))), nil
}