import React from 'react';
import Avatar from 'react-avatar';
import './JobInvitationCard.css';
import Button from '../Button/Button';
import { JobInvitation } from '../../types/leadsType';
import { formatDate } from '../../shared/utils/format';
import LocationIcon from '../Icon/Location';
import WorkIcon from '../Icon/Work';
import PhoneIcon from '../Icon/Phone';
import MailIcon from '../Icon/Mail';

interface JobInvitationCardProps {
  invitation: JobInvitation;
  onAccept: (id: string) => void;
  onDecline: (id: string) => void;
  isAcceptedTab: boolean;
}

const JobInvitationCard: React.FC<JobInvitationCardProps> = ({ invitation, onAccept, onDecline, isAcceptedTab }) => {
  return (
    <div className="invitation">
      <div className="invitation-header">
        <Avatar name={invitation.contactFirstName} size="50" round={true} color="#FF7A01" />
        <div>
          <h2>{invitation.contactFirstName}</h2>
          <p>{formatDate(invitation.dateCreated)}</p>
        </div>
      </div>
      <hr />
      <div className="invitation-details">
        <div>
          <p style={{gap: '10px'}}>
            <span>
              <LocationIcon size={20}/>
            </span>
            <span>
              {invitation.suburb}
            </span>
          </p>
        </div>
        <div>
          <p style={{gap: '10px'}}>
            <span>
              <WorkIcon size={20} /> 
            </span>
            <span>
              {invitation.category}
            </span>
          </p>
        </div>
        <div style={{display: 'flex', alignSelf: 'center', gap: '20px'}}>
          <p>
            <span>
              Job ID: {invitation.id}
            </span>
          </p>
          {isAcceptedTab && 
            <p>
              ${invitation.price} Lead invitation
            </p>
          }
        </div>
      </div>
      <hr />
      {isAcceptedTab ? (
        <div>
          <div className="invitation-details">
            <div>
              <p style={{gap: "10px"}}>
                <span>
                  <PhoneIcon size={18} /> 
                </span>
                <span style={{ color: '#FF7A01' }}>
                  {invitation.contactPhoneNumber || 'N/A'}
                </span>
              </p>
            </div>
            <div>
              <p style={{gap: "10px"}}>
                <span>
                  <MailIcon size={18}/>
                </span>
                <span style={{ color: '#FF7A01' }}>
                  {invitation.contactEmail || 'N/A'}
                </span>
              </p>
            </div>
          </div>
          <p style={{marginLeft: '10px'}}>{invitation.description}</p>
        </div>
      ) : (
        <>
          <p style={{marginLeft: "10px"}}>{invitation.description}</p>
          <hr />
          <div className="fee-and-buttons">
            <div className="buttons">
              <Button borderBottom onClick={() => onAccept(invitation.id)} color="#FF7A01" textColor="#ffffff">Accept</Button>
              <Button borderBottom onClick={() => onDecline(invitation.id)} color="#d8d8d8" textColor="#5e5e5e">Decline</Button>
            </div>
            <div>
              <p>
                <strong style={{color: '#696969'}}>${invitation.price}</strong> Lead invitation
              </p>
            </div>
          </div>
        </>
      )}
    </div>
  );
};

export default JobInvitationCard;
