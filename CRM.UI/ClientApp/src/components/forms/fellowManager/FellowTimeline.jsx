import { useEffect, useState } from "react";
import Timeline from "@mui/lab/Timeline";
import TimelineItem from "@mui/lab/TimelineItem";
import TimelineSeparator from "@mui/lab/TimelineSeparator";
import TimelineConnector from "@mui/lab/TimelineConnector";
import TimelineContent from "@mui/lab/TimelineContent";
import TimelineOppositeContent from "@mui/lab/TimelineOppositeContent";
import TimelineDot from "@mui/lab/TimelineDot";
import Typography from "@mui/material/Typography";
import StoreIcon from "@mui/icons-material/Store";
import RunningWithErrorsIcon from "@mui/icons-material/RunningWithErrors";
import BuildIcon from "@mui/icons-material/Build";
import DoneIcon from "@mui/icons-material/Done";
import PendingIcon from "@mui/icons-material/Pending";
import TicketService from "../../../services/ticket.service";

export default function FellowTimeline() {
  const [ticket, setTicket] = useState(null);
  const [waitingState, setWaitingState] = useState(null);
  const [checkingState, setCheckingState] = useState(null);
  const [inquiryState, setInquiryState] = useState(null);
  const [readyToRepairState, setReadyToRepairState] = useState(null);
  const [repairingState, setRepairingState] = useState(null);
  const [readyState, setReadyState] = useState(null);
  const [doneState, setDoneState] = useState(null);

  useEffect(() => {
    TicketService.get(4).then((result) => {
      debugger;
    });

    // setDeviceTypeSuggestion([
    //   { id: 1, label: "ریش تراش" },
    //   { id: 2, label: "اپیلیدی" },
    //   { id: 3, label: "تریمر" },
    //   { id: 4, label: "سشوار" },
    // ]);
    // setDeviceBrandSuggestion([
    //   { id: 1, label: "Braun" },
    //   { id: 2, label: "Philips" },
    //   { id: 3, label: "Panasonic" },
    //   { id: 4, label: "Remington" },
    // ]);
  }, []);

  return (
    <>
      {ticket === null ? (
        "Loading"
      ) : (
        <>
          <Timeline position="alternate">
            <TimelineItem>
              <TimelineOppositeContent
                sx={{ m: "auto 0" }}
                align="right"
                variant="body2"
                color="text.secondary"
              >
                {waitingState !== null && `${waitingState.persianDate}`}
              </TimelineOppositeContent>
              <TimelineSeparator>
                <TimelineConnector />
                <TimelineDot color={ticket.state === 0 ? "success" : "primary"}>
                  <StoreIcon />
                </TimelineDot>
                <TimelineConnector />
              </TimelineSeparator>
              <TimelineContent sx={{ py: "12px", px: 2 }}>
                <Typography variant="h6" component="span">
                  جدید
                </Typography>
                <Typography>تیکت {ticket.number}</Typography>
              </TimelineContent>
            </TimelineItem>

            <TimelineItem>
              <TimelineOppositeContent
                sx={{ m: "auto 0" }}
                variant="body2"
                color="text.secondary"
              >
                {checkingState !== null && `${checkingState.persianDate}`}
              </TimelineOppositeContent>
              <TimelineSeparator>
                <TimelineConnector />
                <TimelineDot
                  color={
                    checkingState === null
                      ? ""
                      : ticket.state === 1
                      ? "success"
                      : "primary"
                  }
                >
                  <RunningWithErrorsIcon />
                </TimelineDot>
                <TimelineConnector />
              </TimelineSeparator>
              <TimelineContent sx={{ py: "12px", px: 2 }}>
                <Typography variant="h6" component="span">
                  بررسی تعمیرکار
                </Typography>
                <Typography>{ticket?.repairerName}</Typography>
              </TimelineContent>
            </TimelineItem>

            {/* <TimelineItem>
        <TimelineOppositeContent
          sx={{ m: "auto 0" }}
          variant="body2"
          color="text.secondary"
        >
          {inquiryState !== null && `${inquiryState.persianDate}`}
        </TimelineOppositeContent>
        <TimelineSeparator>
          <TimelineConnector />
          <TimelineDot
            color={
              inquiryState === null
                ? ""
                : ticket.state === 2
                ? "success"
                : "primary"
            }
          >
            <RunningWithErrorsIcon />
          </TimelineDot>
          <TimelineConnector />
        </TimelineSeparator>
        <TimelineContent sx={{ py: "12px", px: 2 }}>
          <Typography variant="h6" component="span">
            استعلام
          </Typography>
          <Typography>مثبت</Typography>
        </TimelineContent>
      </TimelineItem>

      <TimelineItem>
        <TimelineOppositeContent
          sx={{ m: "auto 0" }}
          align="right"
          variant="body2"
          color="text.secondary"
        >
          {readyToRepairState !== null && `${readyToRepairState.persianDate}`}
        </TimelineOppositeContent>
        <TimelineSeparator>
          <TimelineConnector />
          <TimelineDot
            color={
              readyToRepairState === null
                ? ""
                : ticket.state === 3
                ? "success"
                : "primary"
            }
          >
            <PendingIcon />
          </TimelineDot>
          <TimelineConnector />
        </TimelineSeparator>
        <TimelineContent sx={{ py: "12px", px: 2 }}>
          <Typography variant="h6" component="span">
            آماده تعمیر
          </Typography>
          <Typography>تیکت 10670</Typography>
        </TimelineContent>
      </TimelineItem>

      <TimelineItem>
        <TimelineOppositeContent
          sx={{ m: "auto 0" }}
          variant="body2"
          color="text.secondary"
        >
          {repairingState !== null && `${repairingState.persianDate}`}
        </TimelineOppositeContent>
        <TimelineSeparator>
          <TimelineConnector />
          <TimelineDot
            color={
              repairingState === null
                ? ""
                : ticket.state === 4
                ? "success"
                : "primary"
            }
          >
            <BuildIcon />
          </TimelineDot>
          <TimelineConnector />
        </TimelineSeparator>
        <TimelineContent sx={{ py: "12px", px: 2 }}>
          <Typography variant="h6" component="span">
            در حال تعمیر
          </Typography>
          <Typography>توسط فرشید</Typography>
        </TimelineContent>
      </TimelineItem>

      <TimelineItem>
        <TimelineOppositeContent
          sx={{ m: "auto 0" }}
          align="right"
          variant="body2"
          color="text.secondary"
        >
          {readyState !== null && `${readyState.persianDate}`}
        </TimelineOppositeContent>
        <TimelineSeparator>
          <TimelineConnector />
          <TimelineDot
            color={
              readyState === null
                ? ""
                : ticket.state === 5 || ticket.state === 6
                ? "success"
                : "primary"
            }
          >
            <BuildIcon />
          </TimelineDot>
          <TimelineConnector />
        </TimelineSeparator>
        <TimelineContent sx={{ py: "12px", px: 2 }}>
          <Typography variant="h6" component="span">
            آماده
          </Typography>
          <Typography>تیکت 10670</Typography>
        </TimelineContent>
      </TimelineItem>

      <TimelineItem>
        <TimelineOppositeContent
          sx={{ m: "auto 0" }}
          variant="body2"
          color="text.secondary"
        >
          {doneState !== null && `${doneState.persianDate}`}
        </TimelineOppositeContent>
        <TimelineSeparator>
          <TimelineConnector />
          <TimelineDot
            color={
              doneState === null
                ? ""
                : ticket.state === 7
                ? "success"
                : "primary"
            }
          >
            <DoneIcon />
          </TimelineDot>
        </TimelineSeparator>
        <TimelineContent sx={{ py: "12px", px: 2 }}>
          <Typography variant="h6" component="span">
            بسته شده
          </Typography>
          <Typography>هزینه نهایی 000و005و1 تومتن</Typography>
        </TimelineContent>
      </TimelineItem> */}
          </Timeline>
        </>
      )}
    </>
  );
}
