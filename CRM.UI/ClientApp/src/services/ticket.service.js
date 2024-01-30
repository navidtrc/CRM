import axios from "axios";



const prerequisite = () => {
  return axios.get("/api/ticket/prerequisite");
};

const get = (id) => {
  return axios.get(`/api/ticket/get?id=${id}`);
};

const TicketService = {
  prerequisite,
  get
}

export default TicketService;
