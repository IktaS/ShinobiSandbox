from turtle import right
from dictionary import *
import csv


class Event:
    def __init__(self, hand, time):
        self.hand = hand
        self.time = time

    def GetType(self):
        return "Unknown"

    def GetValue(self):
        return self.hand


class GestureEvent(Event):
    def __init__(self, hand, time, gesture):
        super().__init__(hand, time)
        self.gesture = gesture

    def GetType(self):
        if isinstance(self.gesture, GestureInputA):
            return "A"
        if isinstance(self.gesture, GestureInputB):
            return "B"
        return "Unknown"

    def GetValue(self):
        return self.gesture

    def __repr__(self) -> str:
        return "{}, {}, {}".format(self.hand, self.gesture, self.time)


class ActionEvent(Event):
    def __init__(self, hand, time, action):
        super().__init__(hand, time)
        self.action = action

    def GetType(self):
        if isinstance(self.action, ActionA):
            return "A"
        if isinstance(self.action, ActionB):
            return "B"
        return "Unknown"

    def GetValue(self):
        return self.action

    def __repr__(self) -> str:
        return "{}, {}, {}".format(self.hand, self.action, self.time)


class Person:
    def __init__(self, file1, file2, file3, time1, time2, time3):
        self.events = [
            self._initEventsData(file1),
            self._initEventsData(file2),
            self._initEventsData(file3),
        ]
        self.times = {
            "Attempt 1": time1,
            "Attempt 2": time2,
            "Attempt 3": time3,
        }

    def _initEventsData(self, file):
        events = []
        print(file)
        with open(file, "r") as file:
            csvreader = csv.reader(file)
            self.header = next(csvreader)
            for row in csvreader:
                hand = row[0]
                gesture = row[1]
                action = row[2]
                time = row[3]
                hasEvent = False
                if gesture != "":
                    events.append(
                        GestureEvent(
                            HandDictionary[hand], time, GestureDictionary[gesture]
                        )
                    )
                    hasEvent = True
                if action != "":
                    events.append(
                        ActionEvent(
                            HandDictionary[hand], time, ActionDictionary[action]
                        )
                    )
                    hasEvent = True
                if not hasEvent:
                    print("wtf something is wrong with the data")
        return events

    def GetHandTotalFrequencyByHand(self):
        first = self.GetHandFrequencyOfSingleAttemptByHand(0)
        second = self.GetHandFrequencyOfSingleAttemptByHand(1)
        third = self.GetHandFrequencyOfSingleAttemptByHand(2)
        return {
            "Right": sum([first["Right"], second["Right"], third["Right"]]),
            "Left": sum([first["Left"], second["Left"], third["Left"]]),
            "Both": sum([first["Both"], second["Both"], third["Both"]]),
        }

    def GetHandFrequencyOfSingleAttemptByHand(self, attempt):
        count = [0, 0, 0]
        for event in self.events[attempt]:
            count[event.hand] += 1
        return {
            "Right": count[0],
            "Left": count[1],
            "Both": count[2],
        }

    def GetHandAverageFrequencyByHand(self):
        first = self.GetHandFrequencyOfSingleAttemptByHand(0)
        second = self.GetHandFrequencyOfSingleAttemptByHand(1)
        third = self.GetHandFrequencyOfSingleAttemptByHand(2)
        return {
            "Right": sum([first["Right"], second["Right"], third["Right"]]) / 3,
            "Left": sum([first["Left"], second["Left"], third["Left"]]) / 3,
            "Both": sum([first["Both"], second["Both"], third["Both"]]) / 3,
        }

    def GetActionTotalFrequencyByAction(self):
        return self._GetTotalFrequencyByEvent(ActionEvent, ActionDictionary.keys())

    def GetActionTotalFrequencyByType(self):
        return self._GetTotalFrequencyByType(ActionEvent)

    def GetActionFrequencyOfSingleAttemptByType(self, attempt):
        return self._GetFrequencyOfSingleAttemptByType(ActionEvent, attempt)

    def GetActionFrequencyOfSingleAttemptByAction(self, attempt):
        return self._GetFrequencyOfSingleAttemptByEvent(
            ActionEvent, ActionDictionary.keys(), attempt
        )

    def GetAverageActionFrequencyByType(self):
        return self._GetAverageFrequencyByType(ActionEvent)

    def GetAverageActionFrequencyByAction(self):
        return self._GetAverageFrequencyByEvent(ActionEvent, ActionDictionary.keys())

    def GetGestureTotalFrequencyByType(self):
        return self._GetTotalFrequencyByType(GestureEvent)

    def GetGestureTotalFrequencyByGesture(self):
        return self._GetTotalFrequencyByEvent(GestureEvent, GestureDictionary.keys())

    def GetGestureFrequencyOfSingleAttemptByType(self, attempt):
        return self._GetFrequencyOfSingleAttemptByType(GestureEvent, attempt)

    def GetGestureFrequencyOfSingleAttemptByGesture(self, attempt):
        return self._GetFrequencyOfSingleAttemptByEvent(
            GestureEvent, GestureDictionary.keys(), attempt
        )

    def GetAverageGestureFrequencyByType(self):
        return self._GetAverageFrequencyByType(GestureEvent)

    def GetAverageGestureFrequencyByGesture(self):
        return self._GetAverageFrequencyByEvent(GestureEvent, GestureDictionary.keys())

    def ToCSV(self, targetpath):
        with open(targetpath, "w", newline="") as f:
            writer = csv.writer(f)
            self._printToCSV(
                writer, "Total Action: ", self.GetActionTotalFrequencyByType()
            )
            self._printToCSV(
                writer,
                "Total Action Attempt 1: ",
                self.GetActionFrequencyOfSingleAttemptByType(0),
            )
            self._printToCSV(
                writer,
                "Total Action Attempt 2: ",
                self.GetActionFrequencyOfSingleAttemptByType(1),
            )
            self._printToCSV(
                writer,
                "Total Action Attempt 3: ",
                self.GetActionFrequencyOfSingleAttemptByType(2),
            )
            self._printToCSV(
                writer, "Average Action: ", self.GetAverageActionFrequencyByType()
            )
            self._printToCSV(
                writer, "Total Gesture: ", self.GetGestureTotalFrequencyByType()
            )
            self._printToCSV(
                writer,
                "Total Gesture Attempt 1: ",
                self.GetGestureFrequencyOfSingleAttemptByType(0),
            )
            self._printToCSV(
                writer,
                "Total Gesture Attempt 2: ",
                self.GetGestureFrequencyOfSingleAttemptByType(1),
            )
            self._printToCSV(
                writer,
                "Total Gesture Attempt 3: ",
                self.GetGestureFrequencyOfSingleAttemptByType(2),
            )
            self._printToCSV(
                writer, "Average Gesture: ", self.GetAverageGestureFrequencyByType()
            )
            self._printToCSV(writer, "Total Hand: ", self.GetHandTotalFrequencyByHand())
            self._printToCSV(
                writer,
                "Total Hand Attempt 1: ",
                self.GetHandFrequencyOfSingleAttemptByHand(0),
            )
            self._printToCSV(
                writer,
                "Total Hand Attempt 2: ",
                self.GetHandFrequencyOfSingleAttemptByHand(1),
            )
            self._printToCSV(
                writer,
                "Total Hand Attempt 3: ",
                self.GetHandFrequencyOfSingleAttemptByHand(2),
            )
            self._printToCSV(
                writer, "Average Hand: ", self.GetHandAverageFrequencyByHand()
            )
            self._printToCSV(
                writer, "Total Action: ", self.GetActionTotalFrequencyByAction()
            )
            self._printToCSV(
                writer,
                "Total Action Attempt 1: ",
                self.GetActionFrequencyOfSingleAttemptByAction(0),
            )
            self._printToCSV(
                writer,
                "Total Action Attempt 2: ",
                self.GetActionFrequencyOfSingleAttemptByAction(1),
            )
            self._printToCSV(
                writer,
                "Total Action Attempt 3: ",
                self.GetActionFrequencyOfSingleAttemptByAction(2),
            )
            self._printToCSV(
                writer, "Average Action: ", self.GetAverageActionFrequencyByAction()
            )
            self._printToCSV(
                writer, "Total Gesture: ", self.GetGestureTotalFrequencyByGesture()
            )
            self._printToCSV(
                writer,
                "Total Gesture Attempt 1: ",
                self.GetGestureFrequencyOfSingleAttemptByGesture(0),
            )
            self._printToCSV(
                writer,
                "Total Gesture Attempt 2: ",
                self.GetGestureFrequencyOfSingleAttemptByGesture(1),
            )
            self._printToCSV(
                writer,
                "Total Gesture Attempt 3: ",
                self.GetGestureFrequencyOfSingleAttemptByGesture(2),
            )
            self._printToCSV(
                writer, "Average Gesture: ", self.GetAverageGestureFrequencyByGesture()
            )
            self._printToCSV(writer, "Time Per Attempt", self.times)

    def _printToCSV(self, writer, name, data):
        writer.writerow([name])
        (keys, values) = zip(*data.items())
        writer.writerow(keys)
        writer.writerow(values)

    def _GetTotalFrequencyByEvent(self, eventType, events):
        res = {}
        map = {}
        if eventType == GestureEvent:
            map = GestureDictionary
        if eventType == ActionEvent:
            map = ActionDictionary
        for k in events:
            res[k] = self._GetTotalFrequencyOfSingleEvent(eventType, map[k])
        return res

    def _GetTotalFrequencyOfSingleEvent(self, eventType, eventVal):
        count = 0
        for attempt in self.events:
            for event in attempt:
                if (
                    isinstance(event, eventType)
                    and event.GetValue() == eventVal
                    and isinstance(event.GetValue(), type(eventVal))
                ):
                    count += 1
        return count

    def _GetTotalFrequencyByType(self, eventType):
        return {
            "A": self._GetTotalFrequencyOfSingleType(eventType, "A"),
            "B": self._GetTotalFrequencyOfSingleType(eventType, "B"),
        }

    def _GetTotalFrequencyOfSingleType(self, eventType, inputType):
        count = 0
        for attempt in self.events:
            for event in attempt:
                if isinstance(event, eventType) and event.GetType() == inputType:
                    count += 1
        return count

    def _GetFrequencyOfSingleAttemptByEvent(self, eventType, events, attempt):
        res = {}
        map = {}
        if eventType == GestureEvent:
            map = GestureDictionary
        if eventType == ActionEvent:
            map = ActionDictionary
        for k in events:
            res[k] = self._GetFrequencyOfSingleEventSingleAttempt(
                eventType, map[k], attempt
            )
        return res

    def _GetFrequencyOfSingleEventSingleAttempt(self, eventType, eventVal, attemptNum):
        count = 0
        for event in self.events[attemptNum]:
            if (
                isinstance(event, eventType)
                and event.GetValue() == eventVal
                and isinstance(event.GetValue(), type(eventVal))
            ):
                count += 1
        return count

    def _GetFrequencyOfSingleAttemptByType(self, eventType, attempt):
        return {
            "A": self._GetFrequencyOfSingleTypeSingleAttempt(eventType, "A", attempt),
            "B": self._GetFrequencyOfSingleTypeSingleAttempt(eventType, "B", attempt),
        }

    def _GetFrequencyOfSingleTypeSingleAttempt(self, eventType, inputType, attemptNum):
        count = 0
        for event in self.events[attemptNum]:
            if isinstance(event, eventType) and event.GetType() == inputType:
                count += 1
        return count

    def _GetAverageFrequencyByType(self, eventType):
        return {
            "A": self._GetAverageFrequencyOfSingleType(eventType, "A"),
            "B": self._GetAverageFrequencyOfSingleType(eventType, "B"),
        }

    def _GetAverageFrequencyOfSingleType(self, eventType, inputType):
        attempts = [
            self._GetFrequencyOfSingleTypeSingleAttempt(eventType, inputType, 0),
            self._GetFrequencyOfSingleTypeSingleAttempt(eventType, inputType, 1),
            self._GetFrequencyOfSingleTypeSingleAttempt(eventType, inputType, 2),
        ]
        return sum(attempts) / len(attempts)

    def _GetAverageFrequencyByEvent(self, eventType, events):
        res = {}
        map = {}
        if eventType == GestureEvent:
            map = GestureDictionary
        if eventType == ActionEvent:
            map = ActionDictionary
        for k in events:
            res[k] = self._GetAverageFrequencyOfSingleEvent(eventType, map[k])
        return res

    def _GetAverageFrequencyOfSingleEvent(self, eventType, eventVal):
        attempts = [
            self._GetFrequencyOfSingleEventSingleAttempt(eventType, eventVal, 0),
            self._GetFrequencyOfSingleEventSingleAttempt(eventType, eventVal, 1),
            self._GetFrequencyOfSingleEventSingleAttempt(eventType, eventVal, 2),
        ]
        return sum(attempts) / len(attempts)
