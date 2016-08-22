Feature: Character listing
    As a Heroes of Storm Administrator
    In order to feel control over system state
    In want to see existing characters in list

Scenario: See characters in listing
    Given there are the following characters in system
        | Name    | Price | Type     | Active | Deleted |
        | Zeratul | 10.00 | Assassin | True   | False   |
    When I am logged in as “Megan”
    Then I should be on “Character listing” page
        And I should see character in list
        | Name    | Price | Type     | Active | Deleted |
        | Zeratul | 10.00 | Assassin | True   | False   |