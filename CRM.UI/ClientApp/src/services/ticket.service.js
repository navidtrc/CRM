import axios from "axios";



const prerequisite = () => {
  return axios.get("/api/ticket/prerequisite");
};

const TicketService = {
  prerequisite
}

export default TicketService;
