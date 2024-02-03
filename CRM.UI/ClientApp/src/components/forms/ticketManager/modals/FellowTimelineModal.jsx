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
import HourglassTopIcon from '@mui/icons-material/HourglassTop';
import PendingIcon from "@mui/icons-material/Pending";
import ContentPasteSearchIcon from '@mui/icons-material/ContentPasteSearch';
import ThumbUpAltIcon from '@mui/icons-material/ThumbUpAlt';
import TicketService from "../../../../services/ticket.service";
import { Box, Modal } from "@mui/material";

const style = {
  position: "absolute",
  top: "50%",
  left: "50%",
  transform: "translate(-50%, -50%)",
  minWidth: 750,
  bgcolor: "background.paper",
  border: "2px solid #000",
  boxShadow: 24,
  p: 4,
  mb: 1,
  "& .MuiTextField-root": { m: 1 },
};

export default function FellowTimelineModal({ data, open, onClose }) {
  const [ticket, setTicket] = useState(null);

  useEffect(() => {
    TicketService.get(data.id).then((result) => {
      setTicket(result.data.Data.Data);
    });
  }, []);

  return (
    <>
      {ticket === null ? (
        "Loading"
      ) : (
        <>
          <Modal
            open={open}
            onClose={onClose}
            aria-labelledby="modal-modal-title"
            aria-describedby="modal-modal-description"
          >
            <Box sx={style}>
              <Timeline position="alternate">
                {/* new = 0 */}
                <TimelineItem>
                  <TimelineOppositeContent
                    sx={{ m: "auto 0" }}
                    align="right"
                    variant="body2"
                    color="text.secondary"
                  >
                    {ticket.PersianDate}
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
                    <Typography>تیکت {ticket.Number}</Typography>
                  </TimelineContent>
                </TimelineItem>

                {/* checking = 1 */}
                {ticket.LastStatus > 0 && (
                  <>
                    <TimelineItem>
                      <TimelineOppositeContent
                        sx={{ m: "auto 0" }}
                        variant="body2"
                        color="text.secondary"
                      >
                        {ticket.PersianDate}
                      </TimelineOppositeContent>
                      <TimelineSeparator>
                        <TimelineConnector />
                        <TimelineDot color="primary">
                          <ContentPasteSearchIcon />
                        </TimelineDot>
                        <TimelineConnector />
                      </TimelineSeparator>
                      <TimelineContent sx={{ py: "12px", px: 2 }}>
                        <Typography variant="h6" component="span">
                          بررسی تعمیرکار
                        </Typography>
                        <Typography>{ticket.RepairerName}</Typography>
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
                        {ticket.PersianDate}
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
                        {ticket.PersianDate}
                      </TimelineOppositeContent>
                      <TimelineSeparator>
                        <TimelineConnector />
                        <TimelineDot color="primary">
                          <HourglassTopIcon />
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
                        {ticket.PersianDate}
                      </TimelineOppositeContent>
                      <TimelineSeparator>
                        <TimelineConnector />
                        <TimelineDot color="primary">
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
                {ticket.Fellows.some(
                  (a) => a.Status === 5 || a.Status === 6
                ) && (
                  <>
                    <TimelineItem>
                      <TimelineOppositeContent
                        sx={{ m: "auto 0" }}
                        align="right"
                        variant="body2"
                        color="text.secondary"
                      >
                        {ticket.PersianDate}
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
                          <DoneIcon />
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
                        {ticket.PersianDate}
                      </TimelineOppositeContent>
                      <TimelineSeparator>
                        <TimelineConnector />
                        <TimelineDot color="primary">
                          <ThumbUpAltIcon />
                        </TimelineDot>
                      </TimelineSeparator>
                      <TimelineContent sx={{ py: "12px", px: 2 }}>
                        <Typography variant="h6" component="span">
                          بسته شده
                        </Typography>
                        <Typography>هزینه نهایی 1,500,000 تومان</Typography>
                      </TimelineContent>
                    </TimelineItem>
                  </>
                )}
              </Timeline>
            </Box>
          </Modal>
        </>
      )}
    </>
  );
}
