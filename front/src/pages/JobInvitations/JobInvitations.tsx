
import React, { useState, useEffect, useCallback } from "react";
import Tabs from "../../components/Tabs/Tabs";
import "./JobInvitations.css";
import JobInvitationCard from "../../components/JobInvitationCard/JobInvitationCard";
import { getLeads, acceptLead, declineLead } from "../../api/leadsApi";
import { JobInvitation } from "../../types/leadsType";
import { toast } from "react-toastify";

const JobInvitations: React.FC = () => {
  const [activeTab, setActiveTab] = useState<string>("Invited");
  const [invitations, setInvitations] = useState<JobInvitation[]>([]);
  const [accepted, setAccepted] = useState<JobInvitation[]>([]);

  const fetchLeads = useCallback(async () => {
    try {
      const data = await getLeads();
      const invitedLeads = data.filter((lead: JobInvitation) => lead.status === 0);
      const acceptedLeads = data.filter((lead: JobInvitation) => lead.status === 1);
      setInvitations(invitedLeads);
      setAccepted(acceptedLeads);
    } catch (error) {
      console.error("Error fetching job invitations", error);
      toast.error("Error fetching job invitations");
    }
  }, []);

  useEffect(() => {
    fetchLeads();
  }, [fetchLeads]);

  const handleAccept = async (id: string) => {
    try {
      await acceptLead(id);
      toast.success("Lead accepted, email sent");
      fetchLeads();
    } catch (error) {
      console.error("Error accepting job invitation", error);
      toast.error("Error accepting job invitation");
    }
  };

  const handleDecline = async (id: string) => {
    try {
      await declineLead(id);
      toast.success("Lead declined");
      fetchLeads();
    } catch (error) {
      console.error("Error declining job invitation", error);
      toast.error("Error declining job invitation");
    }
  };

  const dataToRender = activeTab === "Invited" ? invitations : accepted;

  return (
    <div className="outer-container">
      <div className="job-invitations-container">
        <Tabs tabs={["Invited", "Accepted"]} setActiveTab={setActiveTab}>
          <div key="invited">
            {activeTab === "Invited" &&
              dataToRender.map((invitation) => (
                <JobInvitationCard
                  key={invitation.id}
                  invitation={invitation}
                  onAccept={handleAccept}
                  onDecline={handleDecline}
                  isAcceptedTab={false}
                />
              ))}
          </div>
          <div key="accepted">
            {activeTab === "Accepted" &&
              dataToRender.map((invitation) => (
                <JobInvitationCard
                  key={invitation.id}
                  invitation={invitation}
                  onAccept={handleAccept}
                  onDecline={handleDecline}
                  isAcceptedTab={true}
                />
              ))}
          </div>
        </Tabs>
      </div>
    </div>
  );
};

export default JobInvitations;
