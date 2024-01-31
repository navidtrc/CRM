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

  useEffect(() => {
    TicketService.get(4).then((result) => {
      debugger;
    });

  }, []);

  return (
    <>
      {ticket === null ? (
        "Loading"
      ) : (
        <>
          <Timeline position="alternate">
            {/* new = 0 */}
            <TimelineItem>
              <TimelineOppositeContent
                sx={{ m: "auto 0" }}
                align="right"
                variant="body2"
                color="text.secondary"
              >
                {ticket.persianDate}
              </TimelineOppositeContent>
              <TimelineSeparator>
                <TimelineConnector />
                <TimelineDot color="primary">
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

            {/* checking = 1 */}
            {ticket.status > 0 && (
              <>
                <TimelineItem>
                  <TimelineOppositeContent
                    sx={{ m: "auto 0" }}
                    variant="body2"
                    color="text.secondary"
                  >
                    {ticket.persianDate}
                  </TimelineOppositeContent>
                  <TimelineSeparator>
                    <TimelineConnector />
                    <TimelineDot color="primary">
                      <RunningWithErrorsIcon />
                    </TimelineDot>
                    <TimelineConnector />
                  </TimelineSeparator>
                  <TimelineContent sx={{ py: "12px", px: 2 }}>
                    <Typography variant="h6" component="span">
                      بررسی تعمیرکار
                    </Typography>
                    <Typography>{ticket.repairerName}</Typography>
                  </TimelineContent>
                </TimelineItem>
              </>
            )}

            {/* inquiry = 2 */}
            {ticket.Fellows.some((a) => a.Status === 2) && (
              <>
                <TimelineItem>
                  <TimelineOppositeContent
                    sx={{ m: "auto 0" }}
                    variant="body2"
                    color="text.secondary"
                  >
                    {ticket.persianDate}
                  </TimelineOppositeContent>
                  <TimelineSeparator>
                    <TimelineConnector />
                    <TimelineDot color="warning">
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
              </>
            )}

            {/* readyToRepair = 3 */}
            {ticket.Fellows.some((a) => a.Status === 3) && (
              <>
                <TimelineItem>
                  <TimelineOppositeContent
                    sx={{ m: "auto 0" }}
                    align="right"
                    variant="body2"
                    color="text.secondary"
                  >
                    {ticket.persianDate}
                  </TimelineOppositeContent>
                  <TimelineSeparator>
                    <TimelineConnector />
                    <TimelineDot color="success">
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
              </>
            )}

            {/* repairing = 4 */}
            {ticket.Fellows.some((a) => a.Status === 4) && (
              <>
                <TimelineItem>
                  <TimelineOppositeContent
                    sx={{ m: "auto 0" }}
                    variant="body2"
                    color="text.secondary"
                  >
                    {ticket.persianDate}
                  </TimelineOppositeContent>
                  <TimelineSeparator>
                    <TimelineConnector />
                    <TimelineDot color="success">
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
              </>
            )}

            {/* ready = 5(+) or 6(-) */}
            {ticket.Fellows.some((a) => a.Status === 5 || a.Status === 6) && (
              <>
                <TimelineItem>
                  <TimelineOppositeContent
                    sx={{ m: "auto 0" }}
                    align="right"
                    variant="body2"
                    color="text.secondary"
                  >
                    {ticket.persianDate}
                  </TimelineOppositeContent>
                  <TimelineSeparator>
                    <TimelineConnector />
                    <TimelineDot
                      color={
                        ticket.Fellows.some((a) => a.Status === 5)
                          ? "success"
                          : "error"
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
              </>
            )}

            {/* done = 7 */}
            {ticket.Fellows.some((a) => a.Status === 7) && (
              <>
                <TimelineItem>
                  <TimelineOppositeContent
                    sx={{ m: "auto 0" }}
                    variant="body2"
                    color="text.secondary"
                  >
                    {ticket.persianDate}
                  </TimelineOppositeContent>
                  <TimelineSeparator>
                    <TimelineConnector />
                    <TimelineDot color="primary">
                      <DoneIcon />
                    </TimelineDot>
                  </TimelineSeparator>
                  <TimelineContent sx={{ py: "12px", px: 2 }}>
                    <Typography variant="h6" component="span">
                      بسته شده
                    </Typography>
                    <Typography>هزینه نهایی 000و005و1 تومتن</Typography>
                  </TimelineContent>
                </TimelineItem>
              </>
            )}
          </Timeline>
        </>
      )}
    </>
  );
}
